using Libs.Data;
using Libs.Entity;
using Libs.Models;
using Libs.Service; // Add this for PageList
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions; // Add this for Expression<Func<T, bool>>
using System.Threading.Tasks;

namespace Libs.Repositories
{
    // Interface definition for LichSuThiRepository
    public interface ILichSuThiRepository : IRepository<LichSuThi>
    {
        Task<PageList<LichSuThi>> GetLichSuThiByUserAsync(string userId, int pageNumber = 1, int pageSize = 10);
        Task<LichSuThiDetailModel?> GetLichSuThiDetailAsync(Guid lichSuThiId);
        Task<LichSuThiStatModel> GetLichSuThiStatsAsync(string userId);
        Task<List<CauHoiSaiFrequencyModel>> GetFrequentWrongQuestionsAsync(string userId, int limit = 10);
        Task<bool> DeleteLichSuThiAsync(Guid lichSuThiId, string userId);
        Task<bool> SaveLichSuThiAsync(LichSuThi lichSuThi, List<ChiTietLichSuThi> chiTietList);
        ApplicationDbContext GetDbContext();

        //------//
        Task<List<CauHoi>> GetCauHoiSaiByUserAsync(string userId);
        Task<CauHoi?> GetCauHoiByIdAsync(Guid id);
        Task<List<CauHoiSai>> GetCauHoiSaiListAsync(string userId, Guid cauHoiId);
        Task XoaCauHoiSaisAsync(List<CauHoiSai> list);
        Task AddCauHoiSaiAsync(CauHoiSai cauHoiSai);
        Task SaveChangesAsync();

    }

    // Repository implementation
    public class LichSuThiRepository : RepositoryBase<LichSuThi>, ILichSuThiRepository
    {
        // Constructor calling base constructor
        public LichSuThiRepository(ApplicationDbContext context) : base(context) { }

        // Get user's exam history with pagination
        public async Task<PageList<LichSuThi>> GetLichSuThiByUserAsync(string userId, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.LichSuThis
                .Where(ls => ls.UserId == userId)
                .OrderByDescending(ls => ls.NgayThi)
                .AsQueryable();

            return await PageList<LichSuThi>.CreatePageAsync(query, pageNumber, pageSize);
        }

        // Get detailed exam history
        public async Task<LichSuThiDetailModel?> GetLichSuThiDetailAsync(Guid lichSuThiId)
        {
            // Lấy thông tin cơ bản của lịch sử thi
            var lichSuThi = await _dbContext.LichSuThis
                .FirstOrDefaultAsync(ls => ls.Id == lichSuThiId);

            if (lichSuThi == null)
                return null;

            // Lấy chi tiết câu hỏi và câu trả lời
            var chiTietList = await _dbContext.ChiTietLichSuThis
                .Where(ct => ct.LichSuThiId == lichSuThiId)
                .Include(ct => ct.CauHoi)
                    .ThenInclude(c => c.ChuDe)
                .Include(ct => ct.CauHoi)
                    .ThenInclude(c => c.LoaiBangLai)
                .ToListAsync();

            // Lấy thông tin bài thi
            var baiThi = await _dbContext.BaiThis
                .Include(b => b.ChiTietBaiThis)
                .FirstOrDefaultAsync(b => b.Id == lichSuThi.BaiThiId);

            // Tạo model chi tiết
            var detail = new LichSuThiDetailModel
            {
                LichSuThi = lichSuThi,
                ChiTietList = chiTietList,
                BaiThi = baiThi
            };

            return detail;
        }

        // Get exam statistics
        public async Task<LichSuThiStatModel> GetLichSuThiStatsAsync(string userId)
        {
            var allHistory = await _dbContext.LichSuThis
                .Where(ls => ls.UserId == userId)
                .ToListAsync();

            if (!allHistory.Any())
                return new LichSuThiStatModel();

            var stats = new LichSuThiStatModel
            {
                TongSoBaiThi = allHistory.Count,
                SoBaiThiDat = allHistory.Count(ls => ls.KetQua == "Đậu"),
                SoBaiThiKhongDat = allHistory.Count(ls => ls.KetQua != "Đậu"),
                DiemTrungBinh = allHistory.Average(ls => ls.Diem),
                TyLeDung = allHistory.Average(ls => ls.PhanTramDung / 10),
                BaiThiGanNhat = allHistory.OrderByDescending(ls => ls.NgayThi).FirstOrDefault()
            };

            return stats;
        }

        // Get frequently incorrect questions
        public async Task<List<CauHoiSaiFrequencyModel>> GetFrequentWrongQuestionsAsync(string userId, int limit = 10)
        {
            var cauHoiSai = await _dbContext.CauHoiSais
                .Where(cs => cs.UserId == userId)
                .GroupBy(cs => cs.CauHoiId)
                .Select(group => new CauHoiSaiFrequencyModel
                {
                    CauHoiId = group.Key,
                    SoLanSai = group.Count(),
                    NgaySaiGanNhat = group.Max(cs => cs.NgaySai)
                })
                .OrderByDescending(cs => cs.SoLanSai)
                .Take(limit)
                .ToListAsync();

            // Lấy thông tin chi tiết của câu hỏi
            foreach (var item in cauHoiSai)
            {
                item.CauHoi = await _dbContext.CauHois
                    .Include(c => c.ChuDe)
                    .Include(c => c.LoaiBangLai)
                    .FirstOrDefaultAsync(c => c.Id == item.CauHoiId);
            }

            return cauHoiSai;
        }

        // Delete exam history
        public async Task<bool> DeleteLichSuThiAsync(Guid lichSuThiId, string userId)
        {
            try
            {
                // Kiểm tra quyền xóa
                var lichSuThi = await _dbContext.LichSuThis
                    .FirstOrDefaultAsync(ls => ls.Id == lichSuThiId && ls.UserId == userId);

                if (lichSuThi == null)
                    return false;

                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Xóa chi tiết lịch sử thi
                        var chiTietList = await _dbContext.ChiTietLichSuThis
                            .Where(ct => ct.LichSuThiId == lichSuThiId)
                            .ToListAsync();

                        _dbContext.ChiTietLichSuThis.RemoveRange(chiTietList);
                        _dbContext.LichSuThis.Remove(lichSuThi);

                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return true;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting exam history: {ex.Message}");
                return false;
            }
        }

        // Save exam history
        public async Task<bool> SaveLichSuThiAsync(LichSuThi lichSuThi, List<ChiTietLichSuThi> chiTietList)
        {
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Thêm lịch sử thi
                        await _dbContext.LichSuThis.AddAsync(lichSuThi);
                        await _dbContext.SaveChangesAsync();

                        // Thêm chi tiết lịch sử thi
                        foreach (var chiTiet in chiTietList)
                        {
                            chiTiet.LichSuThiId = lichSuThi.Id;
                            await _dbContext.ChiTietLichSuThis.AddAsync(chiTiet);
                        }

                        // Lưu lại những câu hỏi sai để phục vụ ôn tập
                        foreach (var chiTiet in chiTietList.Where(ct => ct.DungSai == true))
                        {
                            await _dbContext.CauHoiSais.AddAsync(new CauHoiSai
                            {
                                UserId = lichSuThi.UserId ?? string.Empty, // Fix null reference with null-coalescing operator
                                CauHoiId = chiTiet.CauHoiId,
                                NgaySai = DateTime.Now
                            });
                        }

                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return true;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving exam history: {ex.Message}");
                return false;
            }
        }
       

        public async Task<List<CauHoi>> GetCauHoiSaiByUserAsync(string userId)
        {
            return await _dbContext.CauHoiSais
                .Where(c => c.UserId == userId)
                .Include(c => c.CauHoi)
                .ThenInclude(ch => ch.ChuDe)
                .Select(c => c.CauHoi)
                .Distinct()
                .ToListAsync();
        }

        public async Task<CauHoi?> GetCauHoiByIdAsync(Guid id)
        {
            return await _dbContext.CauHois.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CauHoiSai>> GetCauHoiSaiListAsync(string userId, Guid cauHoiId)
        {
            return await _dbContext.CauHoiSais
                .Where(c => c.UserId == userId && c.CauHoiId == cauHoiId)
                .ToListAsync();
        }

        public async Task XoaCauHoiSaisAsync(List<CauHoiSai> list)
        {
            _dbContext.CauHoiSais.RemoveRange(list);
            _dbContext.CauHoiSais.RemoveRange(list);
        }

        public async Task AddCauHoiSaiAsync(CauHoiSai cauHoiSai)
        {
            await _dbContext.CauHoiSais.AddAsync(cauHoiSai);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        // Get database context
        public ApplicationDbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}