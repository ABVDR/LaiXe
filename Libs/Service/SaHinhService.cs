using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Service
{
    public class SaHinhService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISahinhRepository _sahinhRepository;
        public SaHinhService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._sahinhRepository = new SahinhRepository(dbContext);
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<BaiSaHinh>> GetAllBaiSaHinhAsync()
        {
            return await _sahinhRepository.GetAllBaiSaHinhAsync();
        }
        public async Task<BaiSaHinh> GetBaiSaHinhByIdAsync(Guid id)
        {
            return await _sahinhRepository.GetBaiSaHinhByIdAsync(id);
        }
        public async Task<PageList<BaiSaHinh>> GetPagedBaiSaHinh(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            return await _sahinhRepository.GetPagedBaiSaHinh(pageNumber, pageSize, search, sortCol, sortDir);
        }
        public async Task<BaiSaHinh> CreateBaiSaHinhAsync(BaiSaHinh baiSaHinh)
        {
            if (baiSaHinh == null)
            {
                throw new ArgumentNullException(nameof(baiSaHinh));
            }
            if (baiSaHinh.Id == Guid.Empty)
            {
                baiSaHinh.Id = Guid.NewGuid();
            }
            _sahinhRepository.Add(baiSaHinh);
            await _dbContext.SaveChangesAsync();
            return baiSaHinh;
        }
        public async Task<BaiSaHinh> UpdateBaiSaHinhAsync(BaiSaHinh baiSaHinh)
        {
            if (baiSaHinh == null)
            {
                throw new ArgumentNullException(nameof(baiSaHinh));
            }

            var existingBaiSaHinh = await _sahinhRepository.GetBaiSaHinhByIdAsync(baiSaHinh.Id);
            if (existingBaiSaHinh == null)
            {
                return null;
            }
            // Cập nhật các thuộc tính
            existingBaiSaHinh.TenBai = baiSaHinh.TenBai;
            existingBaiSaHinh.Order = baiSaHinh.Order;
            existingBaiSaHinh.NoiDung = baiSaHinh.NoiDung;
            existingBaiSaHinh.LoaiBangLaiId = baiSaHinh.LoaiBangLaiId;
            // Nếu có thêm trường GiaiThich
            _sahinhRepository.Update(existingBaiSaHinh);
            await _dbContext.SaveChangesAsync();
            return existingBaiSaHinh;
        }
        public async Task<bool> DeleteBaiSaHinhAsync(Guid id)
        {
            var baiSaHinh = await _sahinhRepository.GetBaiSaHinhByIdAsync(id);
            if (baiSaHinh == null)
            {
                return false;
            }
            baiSaHinh.isDeleted = true;
            _sahinhRepository.Update(baiSaHinh);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
