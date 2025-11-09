using Libs;
using Libs.Entity;
using Libs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace ET.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ShareController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetShares([FromQuery] string? sortOrder, [FromQuery] string? searchString)
        {
            var sharesQuery = _context.Shares.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                var matchingShares = _context.Shares.Where(s =>
                    s.Content.Contains(searchString) ||
                    (s.Topic != null && s.Topic.Contains(searchString)));

                var matchingReplyShareIds = _context.ShareReplies
                    .Where(r => r.Content.Contains(searchString))
                    .Select(r => r.ShareId)
                    .Distinct();
                sharesQuery = matchingShares
                          .Union(_context.Shares.Where(s => matchingReplyShareIds.Contains(s.Id)));
            }

            sharesQuery = sortOrder == "oldest"
                ? sharesQuery.OrderBy(s => s.CreatedAt)
                : sharesQuery.OrderByDescending(s => s.CreatedAt);

            var shares = await sharesQuery.ToListAsync();
            return Ok(shares);
        }

        [HttpGet("auth-status")]
        public IActionResult AuthStatus()
        {
            return Ok(new
            {
                isAuthenticated = User.Identity.IsAuthenticated,
                userName = User.Identity.IsAuthenticated ? User.Identity.Name : null,
                userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null
            });
        }



        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateShare([FromForm] string Content, [FromForm] string? Topic)
        {
            if (string.IsNullOrEmpty(Content))
                return BadRequest(new { success = false, message = "Nội dung không được để trống." });

            if (string.IsNullOrEmpty(Topic))
                return BadRequest(new { success = false, message = "Vui lòng chọn chủ đề trước khi chia sẻ." });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { success = false, message = "Không xác định được người dùng." });

            var email = User.Identity.Name;
            var userName = email?.Split('@')[0];

            var share = new Share
            {
                Content = Content,
                Topic = Topic,
                UserId = userId,
                UserName = userName,
                CreatedAt = DateTime.Now
            };
            try
            {
                _context.Add(share);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message,
                    detail = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }

            return Ok(new { success = true, share });
        }

        [HttpPost("reply")]
        [Authorize]
        public async Task<IActionResult> CreateReply([FromForm] Guid shareId, [FromForm] string content, [FromForm] Guid? parentReplyId)
        {
            if (string.IsNullOrWhiteSpace(content))
                return BadRequest(new { success = false, message = "Nội dung trả lời không được để trống." });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.Identity.Name;
            var userName = email?.Split('@')[0];

            var reply = new ShareReply
            {
                ShareId = shareId,
                Content = content,
                ParentReplyId = parentReplyId,
                CreatedAt = DateTime.Now,
                UserId = userId,
                UserName = userName
            };

            _context.ShareReplies.Add(reply);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                id = reply.id,
                userId = reply.UserId,
                userName,
                content,
                createdAt = reply.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                shareId = reply.ShareId,
                parentReplyId = parentReplyId
            });
        }

        [HttpPost("upload-image")]
        [Authorize]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }
                var imageUrl = Url.Content("~/uploads/" + fileName);
                return Ok(new { uploaded = true, url = imageUrl });
            }

            return BadRequest("No file uploaded.");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteShare(Guid id)
        {
            var share = await _context.Shares.FindAsync(id);
            if (share == null)
                return NotFound(new { success = false, message = "Không tìm thấy chia sẻ." });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (share.UserId != userId)
                return Forbid(); // Không cho phép nếu không phải chủ sở hữu

            _context.Shares.Remove(share);
            await _context.SaveChangesAsync();
            return Ok(new { success = true });
        }


        [HttpDelete("reply/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReply(Guid id)
        {
            var reply = await _context.ShareReplies.FindAsync(id);
            if (reply == null)
                return NotFound(new { success = false, message = "Không tìm thấy trả lời." });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (reply.UserId != userId)
                return Forbid(); // Không cho phép nếu không phải chủ sở hữu

            _context.ShareReplies.Remove(reply);
            await _context.SaveChangesAsync();
            return Ok(new { success = true });
        }


        [HttpGet("replies/{shareId}")]
        public async Task<IActionResult> GetShareReplies(Guid shareId)
        {
            var replies = await _context.ShareReplies
                .Where(r => r.ShareId == shareId)
                .OrderBy(r => r.CreatedAt)
                .ToListAsync();

            return Ok(replies);
        }

        [HttpPost("report")]
        [Authorize]
        public async Task<IActionResult> Sharereport([FromForm] Guid? shareId, [FromForm] Guid? replyId, [FromForm] string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                return BadRequest(new { success = false, message = "Lý do báo cáo không được để trống." });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid? safeShareId = (shareId.HasValue && shareId != Guid.Empty) ? shareId : null;
            Guid? safeReplyId = (replyId.HasValue && replyId != Guid.Empty) ? replyId : null;

            var report = new ShareReport
            {
                ShareId = safeShareId,
                ShareReplyId = safeReplyId,
                Reason = reason,
                CreatedAt = DateTime.Now,
                UserId = userId
            };
            _context.ShareReports.Add(report);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Báo cáo của bạn đã được gửi." });
        }
        [HttpGet("reports")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _context.ShareReports
                .Include(r => r.Share)
                .Include(r => r.ShareReply)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new {
                    r.Id,
                    Content = r.Share != null ? r.Share.Content :
                              r.ShareReply != null ? r.ShareReply.Content : "<i>Nội dung đã bị xóa</i>",
                    r.Reason,
                    r.CreatedAt,
                    r.UserId,
                    Type = r.Share != null ? "share" : r.ShareReply != null ? "reply" : null,
                    ShareId = r.ShareId,
                    ReplyId = r.ShareReplyId
                })
                .ToListAsync();

            return Ok(reports);
        }

        [HttpPost("delete-content")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContent([FromForm] string type, [FromForm] Guid id)
        {
            if (type == "share")
            {
                var share = await _context.Shares.FindAsync(id);
                if (share != null)
                {
                    // Xóa tất cả báo cáo liên quan đến share
                    var shareReports = _context.ShareReports.Where(r => r.ShareId == id);
                    _context.ShareReports.RemoveRange(shareReports);

                    _context.Shares.Remove(share);
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true });
                }
            }
            else if (type == "reply")
            {
                var reply = await _context.ShareReplies.FindAsync(id);
                if (reply != null)
                {
                    // Xóa tất cả báo cáo liên quan đến reply
                    var replyReports = _context.ShareReports.Where(r => r.ShareReplyId == id);
                    _context.ShareReports.RemoveRange(replyReports);

                    _context.ShareReplies.Remove(reply);
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true });
                }
            }
            return NotFound(new { success = false });
        }

    }
}
