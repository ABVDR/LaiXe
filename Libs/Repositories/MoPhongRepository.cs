using Libs.Data;
using Libs.Entity;
using Libs;
using Microsoft.EntityFrameworkCore;
using Libs.Service;
namespace Libs.Repositories
{
    public interface IMoPhongRepository : IRepository<MoPhong>
    {
        Task<List<MoPhong>> GetMoPhongsByLoaiBangLaiIdAsync(Guid loaId);
        Task<List<MoPhong>> GetAllMoPhongAsync();
        Task<MoPhong> GetMoPhongByIdAsync(Guid id);
        public Task<PageList<MoPhong>> GetPagedMoPhong(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir);


    }

    public class MoPhongRepository : RepositoryBase<MoPhong>, IMoPhongRepository
    {
        public MoPhongRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<MoPhong>> GetMoPhongsByLoaiBangLaiIdAsync(Guid loaiBangLaiId)
        {
            return await _dbContext.MoPhongs
                .Where(x => x.LoaiBangLaiId == loaiBangLaiId)
                .ToListAsync();
        }

        public async Task<List<MoPhong>> GetAllMoPhongAsync()
        {
            return await _dbContext.MoPhongs.ToListAsync();
        }

        public async Task<MoPhong> GetMoPhongByIdAsync(Guid id)
        {
            return await _dbContext.MoPhongs.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<PageList<MoPhong>> GetPagedMoPhong(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            IQueryable<MoPhong> mophongQuery = _dbContext.MoPhongs.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                mophongQuery = mophongQuery.Where(p => p.NoiDung.Contains(search));
            }
            var entityProps = typeof(MoPhong)
            .GetProperties()
            .ToDictionary(p => p.Name.ToLower(), p => p.Name, StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrWhiteSpace(sortCol) && entityProps.ContainsKey(sortCol))
            {
                string actualCol = entityProps[sortCol];
                bool isDescending = sortDir?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false;

                mophongQuery = isDescending
                    ? mophongQuery.OrderByDescending(q => EF.Property<object>(q, actualCol))
                    : mophongQuery.OrderBy(q => EF.Property<object>(q, actualCol));
            }
            else
            {
                mophongQuery = mophongQuery.OrderBy(q => q.Id);
            }
            var moPhongs = await PageList<MoPhong>.CreatePageAsync(mophongQuery, pageNumber, pageSize);
            return moPhongs;
        }
    }
}