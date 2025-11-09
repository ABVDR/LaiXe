using Libs.Data;
using Libs.Entity;
using Libs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IBaiThiRepository : IRepository<BaiThi>
    {
        Task<BaiThi> GetBaiThiWithDetails(Guid id);
        (List<KetQuaBaiThi> ketQuaList, float diem, int tongSoCau, int diemToiThieu) ChamDiem(BaiThi baiThi, Dictionary<Guid, string> answers);
        Task<NopBaiThiResult> XuLyNopBaiThi(SubmitBaiThiRequest request, string userId);
        Task<BaiThi> GetDeThiNgauNhien(Guid loaiBangLaiId);
        Task<BaiThi> GetChiTietBaiThi(Guid id);
        Task<List<BaiThi>> GetDanhSachBaiThi();
        Task<List<BaiThi>> GetDanhSachDeThi(string loaiXe = null);
        Task<List<BaiThi>> GetDeThiByLoaiBangLai(Guid loaiBangLaiId);
        Task<List<CauHoi>> GetCauHoiOnTap(Guid loaiBangLaiId);
        Task<BaiThi> CreateDeThiNgauNhienTheoChuDe(Guid loaiBangLaiId, Guid chuDeId, List<CauHoi> cauHois);
        Task<BaiThi> GetDeThiNgauNhienTheoChuDe(Guid loaiBangLaiId, Guid chuDeId);
        Task<List<CauHoi>> GetCauHoiTaoDeThiAsync(Guid loaiBangLaiId);

        // Thêm các phương thức mới
        void SaveChanges();
        Task SaveChangesAsync();
        Task<BaiThi> GetByIdAsync(Guid id);
        ApplicationDbContext GetDbContext();
    }

    public class BaiThiRepository : RepositoryBase<BaiThi>, IBaiThiRepository
    {
        public BaiThiRepository(ApplicationDbContext context) : base(context) { }

        public async Task<BaiThi> GetBaiThiWithDetails(Guid id)
        {
            return await _dbContext.BaiThis
                .Include(b => b.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                        .ThenInclude(c => c.LoaiBangLai)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public (List<KetQuaBaiThi> ketQuaList, float diem, int tongSoCau, int diemToiThieu) ChamDiem(BaiThi baiThi, Dictionary<Guid, string> answers)
        {
            var ketQuaList = new List<KetQuaBaiThi>();

            foreach (var ct in baiThi.ChiTietBaiThis)
            {
                var userAnswer = answers.ContainsKey(ct.Id) ? answers[ct.Id] : null;
                var isCorrect = !string.IsNullOrEmpty(userAnswer) && ct.CauHoi.DapAnDung == userAnswer[0];

                ketQuaList.Add(new KetQuaBaiThi
                {
                    CauHoiId = ct.CauHoiId,
                    CauTraLoi = string.IsNullOrEmpty(userAnswer) ? ' ' : userAnswer[0],
                    DapAnDung = ct.CauHoi.DapAnDung,
                    DungSai = !isCorrect
                });
            }

            int totalCorrect = ketQuaList.Count(kq => !kq.DungSai);
            float diem = totalCorrect * 10f / baiThi.ChiTietBaiThis.Count;
            int diemToiThieu = baiThi.ChiTietBaiThis.FirstOrDefault()?.CauHoi?.LoaiBangLai?.DiemToiThieu ?? 0;

            return (ketQuaList, diem, baiThi.ChiTietBaiThis.Count, diemToiThieu);
        }

        public async Task<BaiThi> CreateDeThiNgauNhienTheoChuDe(Guid loaiBangLaiId, Guid chuDeId, List<CauHoi> cauHois)
        {
            try
            {
                var loaiBangLai = await _dbContext.LoaiBangLais.FindAsync(loaiBangLaiId);
                var chuDe = await _dbContext.ChuDes.FindAsync(chuDeId);

                if (loaiBangLai == null || chuDe == null)
                    return null;

                // Create a new exam with random questions
                var baiThiId = Guid.NewGuid();
                var baiThi = new BaiThi
                {
                    Id = baiThiId,
                    TenBaiThi = $"Đề ngẫu nhiên - {loaiBangLai.TenLoai} - {chuDe.TenChuDe} - {DateTime.Now:dd/MM/yyyy HH:mm}",
                    ChiTietBaiThis = cauHois
                        .OrderBy(c => Guid.NewGuid()) // Randomize questions
                        .Take(Math.Min(cauHois.Count, 20)) // Take at most 20 questions or all available questions
                        .Select(cauHoi => new ChiTietBaiThi
                        {
                            Id = Guid.NewGuid(),
                            BaiThiId = baiThiId, // Set the BaiThiId explicitly
                            CauHoiId = cauHoi.Id,
                            CauHoi = cauHoi
                        })
                        .ToList()
                };

                // Save the exam to the database
                await _dbContext.BaiThis.AddAsync(baiThi);
                await _dbContext.SaveChangesAsync();

                return baiThi;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateDeThiNgauNhienTheoChuDe: {ex.Message}");
                throw;
            }
        }

        public async Task<List<CauHoi>> GetCauHoiTaoDeThiAsync(Guid loaiBangLaiId)
        {
            try
            {
                // Get questions for this license type with all necessary includes
                return await _dbContext.CauHois
                    .Where(c => c.LoaiBangLaiId == loaiBangLaiId &&
                                !c.isDeleted &&
                                !c.LoaiBangLai.isDeleted)
                    .Include(c => c.ChuDe)
                    .Include(c => c.LoaiBangLai)
                    .OrderBy(c => c.ChuDeId)
                    .ThenByDescending(c => c.DiemLiet)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error in BaiThiRepository.GetCauHoiTaoDeThiAsync: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Return empty list rather than null
                return new List<CauHoi>();
            }
        }

        public async Task<BaiThi> GetDeThiNgauNhienTheoChuDe(Guid loaiBangLaiId, Guid chuDeId)
        {
            try
            {
                // Verify that both the license type and subject exist
                var loaiBangLai = await _dbContext.LoaiBangLais.FindAsync(loaiBangLaiId);
                var chuDe = await _dbContext.ChuDes.FindAsync(chuDeId);

                if (loaiBangLai == null || chuDe == null)
                    return null;

                // Get questions for this license type and subject
                var cauHois = await _dbContext.CauHois
                    .Where(c => c.LoaiBangLaiId == loaiBangLaiId &&
                                c.ChuDeId == chuDeId &&
                                !c.isDeleted &&
                                !c.LoaiBangLai.isDeleted &&
                                !c.ChuDe.isDeleted)
                    .ToListAsync();

                if (cauHois == null || !cauHois.Any())
                    return null;

                // Create a new exam with random questions
                var baiThiId = Guid.NewGuid();
                var baiThi = new BaiThi
                {
                    Id = baiThiId,
                    TenBaiThi = $"Đề ngẫu nhiên - {loaiBangLai.TenLoai} - {chuDe.TenChuDe} - {DateTime.Now:dd/MM/yyyy HH:mm}",
                    ChiTietBaiThis = cauHois
                        .OrderBy(c => Guid.NewGuid()) // Randomize questions
                        .Take(Math.Min(cauHois.Count, 20)) // Take at most 20 questions or all available questions
                        .Select(cauHoi => new ChiTietBaiThi
                        {
                            Id = Guid.NewGuid(),
                            BaiThiId = baiThiId, // Set the BaiThiId explicitly
                            CauHoiId = cauHoi.Id,
                            CauHoi = cauHoi
                        })
                        .ToList()
                };

                // Save the exam to the database using a transaction
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        await _dbContext.BaiThis.AddAsync(baiThi);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                return baiThi;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDeThiNgauNhienTheoChuDe: {ex.Message}");
                throw;
            }
        }

        public async Task<NopBaiThiResult> XuLyNopBaiThi(SubmitBaiThiRequest request, string? userId)
        {
            var baiThi = await _dbContext.BaiThis
                .Include(b => b.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                        .ThenInclude(c => c.LoaiBangLai)
                .FirstOrDefaultAsync(b => b.Id == request.BaiThiId);

            if (baiThi == null)
                return new NopBaiThiResult { Success = false, Message = "Không tìm thấy bài thi." };

            var (ketQuaList, diem, tongSoCau, diemToiThieu) = ChamDiem(baiThi, request.Answers);
            var ketQua = diem >= diemToiThieu ? "Đậu" : "Không Đạt";

            int soCauDung = ketQuaList.Count(kq => !kq.DungSai);

            bool macLoiNghiemTrong = ketQuaList.Any(kq =>
                kq.DungSai && baiThi.ChiTietBaiThis.First(ct => ct.CauHoiId == kq.CauHoiId).CauHoi.DiemLiet);

            int soCauLoiNghiemTrong = ketQuaList.Count(kq =>
                kq.DungSai && baiThi.ChiTietBaiThis.First(ct => ct.CauHoiId == kq.CauHoiId).CauHoi.DiemLiet);

            if (!string.IsNullOrEmpty(userId))
                await LuuLichSuThiAsync(userId, baiThi, ketQuaList, tongSoCau, diem, ketQua, macLoiNghiemTrong);

            return new NopBaiThiResult
            {
                Success = true,
                BaiThiId = baiThi.Id,
                KetQuaList = ketQuaList,
                SoCauDung = soCauDung,
                TongSoCau = tongSoCau,
                KetQua = ketQua,
                MacLoiNghiemTrong = macLoiNghiemTrong,
                SoCauLoiNghiemTrong = soCauLoiNghiemTrong,
                TongDiem = (int)diem
            };
        }

        private async Task LuuLichSuThiAsync(string userId, BaiThi baiThi, List<KetQuaBaiThi> ketQuaList,
            int tongSoCau, float diem, string ketQua, bool macLoiNghiemTrong)
        {
            var lichSuThi = new LichSuThi
            {
                UserId = userId,
                BaiThiId = baiThi.Id,
                TenBaiThi = baiThi.TenBaiThi.Length > 100 ? baiThi.TenBaiThi[..100] : baiThi.TenBaiThi,
                NgayThi = DateTime.Now,
                TongSoCau = tongSoCau,
                SoCauDung = ketQuaList.Count(k => !k.DungSai),
                PhanTramDung = diem * 10,
                Diem = (int)diem,
                KetQua = ketQua.Length > 20 ? ketQua[..20] : ketQua,
                MacLoiNghiemTrong = macLoiNghiemTrong
            };

            await _dbContext.LichSuThis.AddAsync(lichSuThi);
            await _dbContext.SaveChangesAsync();

            foreach (var kq in ketQuaList)
            {
                await _dbContext.ChiTietLichSuThis.AddAsync(new ChiTietLichSuThi
                {
                    LichSuThiId = lichSuThi.Id,
                    CauHoiId = kq.CauHoiId,
                    CauTraLoi = kq.CauTraLoi,
                    DungSai = kq.DungSai
                });

                if (kq.DungSai)
                    await XuLyCauHoiSaiAsync(userId, kq.CauHoiId);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task XuLyCauHoiSaiAsync(string userId, Guid cauHoiId)
        {
            var existing = await _dbContext.CauHoiSais
                .FirstOrDefaultAsync(x => x.UserId == userId && x.CauHoiId == cauHoiId);

            if (existing != null)
                existing.NgaySai = DateTime.Now;
            else
                await _dbContext.CauHoiSais.AddAsync(new CauHoiSai
                {
                    UserId = userId,
                    CauHoiId = cauHoiId,
                    NgaySai = DateTime.Now
                });
        }

        public async Task<BaiThi> GetDeThiNgauNhien(Guid loaiBangLaiId)
        {
            return await _dbContext.BaiThis
                .Where(bt => bt.ChiTietBaiThis
                    .Any(ct => ct.CauHoi != null && ct.CauHoi.LoaiBangLaiId == loaiBangLaiId))
                .OrderBy(x => Guid.NewGuid())
                .Include(bt => bt.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                .FirstOrDefaultAsync();
        }

        public async Task<BaiThi> GetChiTietBaiThi(Guid id)
        {
            return await _dbContext.BaiThis
                .Include(bt => bt.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                        .ThenInclude(c => c.LoaiBangLai)
                .Include(bt => bt.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                        .ThenInclude(c => c.ChuDe)
                .FirstOrDefaultAsync(bt => bt.Id == id);
        }

        public async Task<List<BaiThi>> GetDanhSachBaiThi()
        {
            return await _dbContext.BaiThis
                .Include(bt => bt.ChiTietBaiThis)
                .ToListAsync();
        }

        public async Task<List<BaiThi>> GetDanhSachDeThi(string loaiXe = null)
        {
            var query = _dbContext.BaiThis
                .Include(bt => bt.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                        .ThenInclude(c => c.LoaiBangLai)
                .Include(bt => bt.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                        .ThenInclude(c => c.ChuDe)
                .AsQueryable();

            if (!string.IsNullOrEmpty(loaiXe))
            {
                query = query.Where(bt => bt.ChiTietBaiThis
                    .Any(ct => !ct.CauHoi.LoaiBangLai.isDeleted && ct.CauHoi.LoaiBangLai.LoaiXe == loaiXe));
            }

            return await query.ToListAsync();
        }

        public async Task<List<BaiThi>> GetDeThiByLoaiBangLai(Guid loaiBangLaiId)
        {
            return await _dbContext.BaiThis
                .Where(bt => bt.ChiTietBaiThis
                    .Any(ct => ct.CauHoi.LoaiBangLaiId == loaiBangLaiId && !ct.CauHoi.LoaiBangLai.isDeleted))
                .Include(bt => bt.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                        .ThenInclude(c => c.LoaiBangLai)
                .Include(bt => bt.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                        .ThenInclude(c => c.ChuDe)
                .ToListAsync();
        }

        public async Task<List<CauHoi>> GetCauHoiOnTap(Guid loaiBangLaiId)
        {
            try
            {
                // First, check if loaiBangLaiId is valid
                var loaiBangLai = await _dbContext.LoaiBangLais
                    .FirstOrDefaultAsync(l => l.Id == loaiBangLaiId && !l.isDeleted);

                if (loaiBangLai == null)
                {
                    Console.WriteLine($"LoaiBangLai with ID {loaiBangLaiId} not found or is deleted");
                    return new List<CauHoi>(); // Return empty list rather than null
                }

                // Use explicit loading pattern to prevent navigation property issues
                var cauHois = await _dbContext.CauHois
                    .Where(c => c.LoaiBangLaiId == loaiBangLaiId &&
                                !c.isDeleted)
                    .Include(c => c.ChuDe)
                    .Include(c => c.LoaiBangLai)
                    .OrderBy(c => c.ChuDeId)
                    .ToListAsync();

                // Filter out any questions with deleted related entities
                cauHois = cauHois
                    .Where(c => c.LoaiBangLai != null && !c.LoaiBangLai.isDeleted &&
                                c.ChuDe != null && !c.ChuDe.isDeleted)
                    .ToList();

                // Log the count for debugging
                Console.WriteLine($"Repository: Found {cauHois.Count} questions for loaiBangLaiId: {loaiBangLaiId}");

                return cauHois;
            }
            catch (Exception ex)
            {
                // Log the exception with detailed information
                Console.WriteLine($"Error in BaiThiRepository.GetCauHoiOnTap: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                Console.WriteLine($"InnerException: {ex.InnerException?.Message}");

                // Return an empty list rather than null
                return new List<CauHoi>();
            }
        }

        // Thêm các phương thức mới
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BaiThi> GetByIdAsync(Guid id)
        {
            return await _dbContext.BaiThis
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public ApplicationDbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}