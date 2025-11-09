using Libs.Data;
using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libs.Service
{
    public class ChuDeService
    {
        private readonly IChuDeRepository _chuDeRepository;

        public ChuDeService(IChuDeRepository chuDeRepository)
        {
            _chuDeRepository = chuDeRepository;
        }

        public async Task<List<ChuDe>> GetDanhSachChuDe()
        {
            return await _chuDeRepository.GetDanhSachChuDeAsync();
        }

        public async Task<List<CauHoi>> GetCauHoiTheoChuDe(Guid loaiBangLaiId, Guid chuDeId)
        {
            try
            {
                var result = await _chuDeRepository.GetCauHoiTheoChuDeAsync(loaiBangLaiId, chuDeId);
                return result ?? new List<CauHoi>(); // Đảm bảo không trả về null
            }
            catch (Exception ex)
            {
                // Log lỗi
                Console.WriteLine($"Error in ChuDeService.GetCauHoiTheoChuDe: {ex.Message}");
                // Trả về empty list thay vì throw exception
                return new List<CauHoi>();
            }
        }

        public async Task<string> GetTenChuDeById(Guid chuDeId)
        {
            return await _chuDeRepository.GetTenChuDeByIdAsync(chuDeId);
        }



        public async Task<IEnumerable<ChuDe>> GetAllAsync()
        {
            return await Task.Run(() => _chuDeRepository.GetAll());
        }

        public async Task<IEnumerable<ChuDe>> GetAllNotDeletedAsync()
        {
            return await _chuDeRepository.GetAllNotDelete();
        }

        public async Task<ChuDe?> GetByIdAsync(Guid id)
        {
            return await _chuDeRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(ChuDe chude)
        {
            _chuDeRepository.Add(chude);
            _chuDeRepository.Save();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(ChuDe chude)
        {
            _chuDeRepository.Update(chude);
            _chuDeRepository.Save();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _chuDeRepository.GetById(id);
            if (entity != null)
            {
                _chuDeRepository.Delete(entity);
                _chuDeRepository.Save();
            }
            await Task.CompletedTask;
        }
    }
}
