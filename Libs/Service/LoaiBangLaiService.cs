using Libs.Entity;
using Libs.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libs.Service
{
    public class LoaiBangLaiService
    {
        private readonly ILoaiBangLaiRepository _loaiBangLaiRepository;
        private readonly ApplicationDbContext _dbContext;


        public LoaiBangLaiService(ILoaiBangLaiRepository loaiBangLaiRepository, ApplicationDbContext dbContext)
        {
            _loaiBangLaiRepository = new LoaiBangLaiRepository(dbContext);
            this._dbContext = dbContext;

        }

        public async Task<List<LoaiBangLai>> GetXeMayAsync()
        {
            return await _loaiBangLaiRepository.GetLoaiBangLaiXeMayAsync();
        }

        public async Task<List<LoaiBangLai>> GetOToAsync()
        {
            return await _loaiBangLaiRepository.GetLoaiBangLaiOToAsync();
        }

        public async Task<List<LoaiBangLai>> GetDanhSachLoaiBangLaiAsync()
        {
            return await _loaiBangLaiRepository.GetDanhSachLoaiBangLaiAsync();
        }

        public async Task<LoaiBangLai> GetByIdAsync(Guid id)
        {
            return await _loaiBangLaiRepository.GetLoaiBangLaiByIdAsync(id);
        }

        public async Task<(LoaiBangLai, List<ChuDe>)> GetChuDeByLoaiBangLaiAsync(Guid id)
        {
            return await _loaiBangLaiRepository.GetChuDeByLoaiBangLaiAsync(id);
        }
        public List<LoaiBangLai> GetAllLoaiBangLais()
        {
            return _loaiBangLaiRepository.GetAll().ToList();
        }

        public async Task<PageList<LoaiBangLai>> GetPagedLoaiBangLai(int pageNumber, int pageSize, string? search, string? sortCol, string sortDir)
        {
            return await _loaiBangLaiRepository.GetPagedLoaiBangLai(pageNumber, pageSize, search, sortCol, sortDir);
        }

        public async Task<LoaiBangLai> CreateLoaiBangLaiAsync(LoaiBangLai loaiBangLai)
        {
            if (loaiBangLai == null)
            {
                throw new ArgumentNullException(nameof(loaiBangLai));
            }

            if (loaiBangLai.Id == Guid.Empty)
            {
                loaiBangLai.Id = Guid.NewGuid();
            }

            _dbContext.LoaiBangLais.Add(loaiBangLai);
            await _dbContext.SaveChangesAsync();
            return loaiBangLai;
        }


        public async Task<LoaiBangLai?> UpdateLoaiBangLaiAsync(LoaiBangLai loaiBangLai)
        {
            if (loaiBangLai == null)
            {
                throw new ArgumentNullException(nameof(loaiBangLai));
            }

            var existing = await _dbContext.LoaiBangLais.FindAsync(loaiBangLai.Id);
            if (existing == null || existing.isDeleted)
            {
                return null;
            }

            // Cập nhật thuộc tính
            existing.TenLoai = loaiBangLai.TenLoai;
            existing.MoTa = loaiBangLai.MoTa;
            existing.LoaiXe = loaiBangLai.LoaiXe;
            existing.ThoiGianThi = loaiBangLai.ThoiGianThi;
            existing.DiemToiThieu = loaiBangLai.DiemToiThieu;

            _dbContext.LoaiBangLais.Update(existing);
            await _dbContext.SaveChangesAsync();

            return existing;
        }


        public async Task<bool> DeleteLoaiBangLaiAsync(Guid id)
        {
            var loaiBangLai = await _loaiBangLaiRepository.GetLoaiBangLaiById(id);
            if (loaiBangLai == null)
            {
                return false;
            }
            loaiBangLai.isDeleted = true;
            Console.WriteLine($"Deleting LoaiBangLai with ID: {id}");
            Console.WriteLine($"LoaiBangLai Details: {loaiBangLai.TenLoai}, {loaiBangLai.MoTa}, {loaiBangLai.LoaiXe}, {loaiBangLai.ThoiGianThi}, {loaiBangLai.isDeleted}");
            _loaiBangLaiRepository.Update(loaiBangLai);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<LoaiBangLai>> GetLoaiBangLaiHasMoPhong()
        {
            return await _loaiBangLaiRepository.GetLoaiBangLaiHasMoPhong();
        }
    }
}
