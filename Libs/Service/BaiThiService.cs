using Libs.Entity;
using Libs.Models;
using Libs.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data; // Thêm namespace này cho IsolationLevel
using System.Linq;
using System.Threading.Tasks;

namespace Libs.Service
{
    public class BaiThiService
    {
        private readonly IBaiThiRepository _baiThiRepository;

        public BaiThiService(IBaiThiRepository baiThiRepository)
        {
            _baiThiRepository = baiThiRepository;
        }

        public async Task<BaiThi> CreateDeThiNgauNhienTheoChuDe(Guid loaiBangLaiId, Guid chuDeId, List<CauHoi> cauHois)
        {
            return await _baiThiRepository.CreateDeThiNgauNhienTheoChuDe(loaiBangLaiId, chuDeId, cauHois);
        }

        public async Task<BaiThi> GetBaiThiWithDetails(Guid id)
        {
            return await _baiThiRepository.GetBaiThiWithDetails(id);
        }

        public (List<KetQuaBaiThi> ketQuaList, float diem, int tongSoCau, int diemToiThieu)
            ChamDiem(BaiThi baiThi, Dictionary<Guid, string> answers)
        {
            return _baiThiRepository.ChamDiem(baiThi, answers);
        }

        public async Task<NopBaiThiResult> XuLyNopBaiThi(SubmitBaiThiRequest request, string userId)
        {
            return await _baiThiRepository.XuLyNopBaiThi(request, userId);
        }

        public async Task<BaiThi> GetDeThiNgauNhien(Guid loaiBangLaiId)
        {
            return await _baiThiRepository.GetDeThiNgauNhien(loaiBangLaiId);
        }

        public async Task<BaiThi> GetChiTietBaiThi(Guid id)
        {
            return await _baiThiRepository.GetChiTietBaiThi(id);
        }

        public async Task<List<BaiThi>> GetDanhSachBaiThi()
        {
            return await _baiThiRepository.GetDanhSachBaiThi();
        }

        // Sửa lại phương thức AddAsync để lưu đề thi đúng cách
        public async Task AddAsync(BaiThi baiThi)
        {
            // Ensure DbContext is accessible
            var dbContext = _baiThiRepository.GetDbContext();
            if (dbContext == null)
            {
                throw new InvalidOperationException("Database context không khả dụng");
            }

            // Use explicit transaction to ensure data consistency
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Ensure BaiThiId is set for each ChiTietBaiThi
                    foreach (var chiTiet in baiThi.ChiTietBaiThis)
                    {
                        if (chiTiet.BaiThiId == Guid.Empty)
                        {
                            chiTiet.BaiThiId = baiThi.Id;
                        }
                    }

                    _baiThiRepository.Add(baiThi);
                    await _baiThiRepository.SaveChangesAsync(); // Use async version for better performance

                    transaction.Commit();
                    Console.WriteLine($"Đã lưu đề thi {baiThi.Id} thành công với {baiThi.ChiTietBaiThis.Count} câu hỏi");
                }
                catch (DbUpdateException dbEx)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Database error when creating exam: {dbEx.Message}");

                    if (dbEx.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {dbEx.InnerException.Message}");
                        Console.WriteLine($"Stack trace: {dbEx.StackTrace}");
                    }

                    throw new ApplicationException("Không thể lưu đề thi vào cơ sở dữ liệu. Chi tiết: " + dbEx.Message, dbEx);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"General error creating exam: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    throw new ApplicationException("Lỗi khi tạo đề thi: " + ex.Message, ex);
                }
            }
        }

        public async Task<List<BaiThi>> GetDanhSachDeThi(string loaiXe = null)
        {
            try
            {
                var list = await _baiThiRepository.GetDanhSachDeThi(loaiXe);

                // Loại bỏ các đề thi không có câu hỏi để tránh lỗi
                list = list.Where(bt => bt.ChiTietBaiThis != null && bt.ChiTietBaiThis.Any()).ToList();

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BaiThiService.GetDanhSachDeThi: {ex.Message}");
                return new List<BaiThi>();
            }
        }

        public async Task<List<CauHoi>> GetCauHoiTaoDeThiAsync(Guid loaiBangLaiId)
        {
            try
            {
                if (loaiBangLaiId == Guid.Empty)
                {
                    return new List<CauHoi>();
                }

                var cauHois = await _baiThiRepository.GetCauHoiTaoDeThiAsync(loaiBangLaiId);

                return cauHois?
                    .OrderBy(c => c.ChuDe?.TenChuDe)
                    .ThenByDescending(c => c.DiemLiet)
                    .ToList() ?? new List<CauHoi>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCauHoiTaoDeThiAsync: {ex.Message}");
                return new List<CauHoi>();
            }
        }

        public async Task<List<BaiThi>> GetDeThiByLoaiBangLai(Guid loaiBangLaiId)
        {
            return await _baiThiRepository.GetDeThiByLoaiBangLai(loaiBangLaiId);
        }

        public async Task<List<CauHoi>> GetCauHoiOnTap(Guid loaiBangLaiId)
        {
            try
            {
                if (loaiBangLaiId == Guid.Empty)
                {
                    return new List<CauHoi>();
                }

                var list = await _baiThiRepository.GetCauHoiOnTap(loaiBangLaiId);
                return list ?? new List<CauHoi>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BaiThiService.GetCauHoiOnTap: {ex.Message}");
                return new List<CauHoi>();
            }
        }

        public async Task Delete(BaiThi baiThi)
        {
            try
            {
                _baiThiRepository.Delete(baiThi);
                _baiThiRepository.SaveChanges(); // Cần gọi SaveChanges sau khi xóa
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting exam: {ex.Message}");
                throw;
            }
        }

        public async Task<BaiThi> GetById(Guid id)
        {
            return await _baiThiRepository.GetByIdAsync(id); // Sử dụng phương thức async
        }
    }
}