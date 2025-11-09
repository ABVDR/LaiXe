using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libs.Entity;
using Libs.Repositories;

namespace Libs.Service
{
    public class MoPhongService
    {
        private ApplicationDbContext dbContext;
        private IMoPhongRepository moPhongRepository;
        public MoPhongService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.moPhongRepository = new MoPhongRepository(dbContext);
        }
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<MoPhong>> GetMoPhongByLoaiBangLaiIdAsync(Guid loaiBangLai)
        {
            return await moPhongRepository.GetMoPhongsByLoaiBangLaiIdAsync(loaiBangLai);
        }

        public async Task<List<MoPhong>> GetAllMoPhongAsync()
        {
            return await moPhongRepository.GetAllMoPhongAsync();
        }

        public async Task<MoPhong> GetMoPhongByIdAsync(Guid id)
        {
            return await moPhongRepository.GetMoPhongByIdAsync(id);
        }
        public async Task<PageList<MoPhong>> GetPagedMoPhong(int pageNumber, int pageSize, string? search, string? sortCol, string sortDir)
        {
            return await moPhongRepository.GetPagedMoPhong(pageNumber, pageSize, search, sortCol, sortDir);
        }
        public async Task<MoPhong> CreateMoPhongAsync(MoPhong moPhong)
        {
            if (moPhong.Id == Guid.Empty)
            {
                moPhong.Id = Guid.NewGuid();
            }

            moPhongRepository.Add(moPhong);
            await dbContext.SaveChangesAsync();
            return moPhong;
        }

        public async Task<MoPhong> UpdateMoPhongAsync(MoPhong moPhong)
        {
            var existingMoPhong = await moPhongRepository.GetMoPhongByIdAsync(moPhong.Id);
            if (existingMoPhong == null)
            {
                return null;
            }

            existingMoPhong.NoiDung = moPhong.NoiDung;
            existingMoPhong.VideoUrl = moPhong.VideoUrl;
            existingMoPhong.DapAn = moPhong.DapAn;
            existingMoPhong.LoaiBangLaiId = moPhong.LoaiBangLaiId;

            moPhongRepository.Update(existingMoPhong);
            await dbContext.SaveChangesAsync();
            return existingMoPhong;
        }

        public async Task<bool> DeleteMoPhongAsync(Guid id)
        {
            var moPhong = await moPhongRepository.GetMoPhongByIdAsync(id);
            if (moPhong == null)
            {
                return false;
            }

            moPhongRepository.Delete(moPhong);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
