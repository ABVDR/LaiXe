using Libs.Data;
using Libs.Entity;
using Libs;
using Microsoft.EntityFrameworkCore;
namespace Libs.Repositories
{
    public interface IChuDeRepository : IRepository<ChuDe>
    {
        Task<List<ChuDe>> GetDanhSachChuDeAsync();
        Task<List<CauHoi>> GetCauHoiTheoChuDeAsync(Guid loaiBangLaiId, Guid chuDeId);
        Task<string> GetTenChuDeByIdAsync(Guid chuDeId);
        Task<IEnumerable<ChuDe>> GetAllNotDelete();
    }

    public class ChuDeRepository : RepositoryBase<ChuDe>, IChuDeRepository
    {
            public ChuDeRepository(ApplicationDbContext context) : base(context) { }

            public async Task<IEnumerable<ChuDe>> GetAllNotDelete()
            {
                return await _dbContext.ChuDes
                    .Where(c => !c.isDeleted)
                    .ToListAsync();
            }
        public async Task<List<ChuDe>> GetDanhSachChuDeAsync()
            {
                return await _dbContext.ChuDes
                    .Where(cd => !cd.isDeleted)
                    .ToListAsync();
            }
        


        public async Task<string> GetTenChuDeByIdAsync(Guid chuDeId)
            {
                return await _dbContext.ChuDes
                    .Where(cd => cd.Id == chuDeId && !cd.isDeleted)
                    .Select(cd => cd.TenChuDe)
                    .FirstOrDefaultAsync() ?? "Không rõ chủ đề";
            }

            public async Task<List<CauHoi>> GetCauHoiTheoChuDeAsync(Guid loaiBangLaiId, Guid chuDeId)
            {
                return await _dbContext.CauHois
                    .Where(c => c.LoaiBangLaiId == loaiBangLaiId &&
                                c.ChuDeId == chuDeId &&
                                !c.LoaiBangLai.isDeleted &&
                                !c.ChuDe.isDeleted)
                    .OrderBy(c => c.NoiDung)
                    .ToListAsync();
            }
        }
    }