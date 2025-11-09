using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libs.Data;
using Libs.Entity;
using Libs.Service;
using Microsoft.EntityFrameworkCore;
namespace Libs.Repositories
{
    public interface ISahinhRepository : IRepository<BaiSaHinh>
    {
        Task<List<BaiSaHinh>> GetAllBaiSaHinhAsync();
        Task<BaiSaHinh> GetBaiSaHinhByIdAsync(Guid id);
        public Task<PageList<BaiSaHinh>> GetPagedBaiSaHinh(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir);
    }
    public class SahinhRepository : RepositoryBase<BaiSaHinh>, ISahinhRepository
    {
        public SahinhRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<BaiSaHinh>> GetAllBaiSaHinhAsync()
        {
            return await _dbContext.BaiSaHinhs
                .Where(b => !b.isDeleted)
                .OrderBy(b => b.Order)
                .Select(b => new BaiSaHinh
                {
                    Id = b.Id,
                    TenBai = b.TenBai,
                    Order = b.Order,
                    NoiDung = b.NoiDung,
                    LoaiBangLai = new LoaiBangLai
                    {
                        Id = b.LoaiBangLai.Id,
                        TenLoai = b.LoaiBangLai.TenLoai
                    }
                })
                .ToListAsync();
        }

        public async Task<BaiSaHinh> GetBaiSaHinhByIdAsync(Guid id)
        {
            return await _dbContext.BaiSaHinhs
                  .Include(b => b.LoaiBangLai)
                  .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<PageList<BaiSaHinh>> GetPagedBaiSaHinh(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            IQueryable<BaiSaHinh> BaiSaHinhQuery = _dbContext.BaiSaHinhs
                .Include(x => x.LoaiBangLai) // Thêm dòng này để include navigation property
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                BaiSaHinhQuery = BaiSaHinhQuery.Where(p => p.TenBai.Contains(search) || p.NoiDung.Contains(search));
            }
            BaiSaHinhQuery = BaiSaHinhQuery.Where(x => !x.isDeleted);

            var entityProps = typeof(BaiSaHinh)
                .GetProperties()
                .ToDictionary(p => p.Name.ToLower(), p => p.Name, StringComparer.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(sortCol) && entityProps.ContainsKey(sortCol))
            {
                string actualCol = entityProps[sortCol];
                bool isDescending = sortDir?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false;
                BaiSaHinhQuery = isDescending
                    ? BaiSaHinhQuery.OrderByDescending(q => EF.Property<object>(q, actualCol))
                    : BaiSaHinhQuery.OrderBy(q => EF.Property<object>(q, actualCol));
            }
            else
            {
                BaiSaHinhQuery = BaiSaHinhQuery.OrderBy(q => q.Id);
            }
            return await PageList<BaiSaHinh>.CreatePageAsync(BaiSaHinhQuery, pageNumber, pageSize);
        }
    }
}
