using Libs;
using Libs.Data;
using Libs.Entity;
using Libs.Service;
using Microsoft.EntityFrameworkCore;
namespace Libs.Repositories
{
    public interface ILoaiBangLaiRepository : IRepository<LoaiBangLai>
    {
        Task<List<LoaiBangLai>> GetLoaiBangLaiXeMayAsync();
        Task<List<LoaiBangLai>> GetLoaiBangLaiOToAsync();
        Task<List<LoaiBangLai>> GetDanhSachLoaiBangLaiAsync();
        Task<LoaiBangLai> GetLoaiBangLaiByIdAsync(Guid loaiBangLaiId);
        Task<(LoaiBangLai, List<ChuDe>)> GetChuDeByLoaiBangLaiAsync(Guid loaiBangLaiId);
        public Task<PageList<LoaiBangLai>> GetPagedLoaiBangLai(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir);
        Task<LoaiBangLai> GetLoaiBangLaiById(Guid id);
        Task<List<LoaiBangLai>> GetLoaiBangLaiHasMoPhong();
    }

    public class LoaiBangLaiRepository : RepositoryBase<LoaiBangLai>, ILoaiBangLaiRepository
    {
        public LoaiBangLaiRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<LoaiBangLai>> GetLoaiBangLaiXeMayAsync()
        {
            return await _dbContext.LoaiBangLais
                .Where(l => l.LoaiXe == "Xe máy" && !l.isDeleted)
                .ToListAsync();
        }

        public async Task<List<LoaiBangLai>> GetLoaiBangLaiOToAsync()
        {
            return await _dbContext.LoaiBangLais
                .Where(l => l.LoaiXe == "Xe oto" && !l.isDeleted)
                .ToListAsync();
        }

        public async Task<List<LoaiBangLai>> GetDanhSachLoaiBangLaiAsync()
        {
            return await _dbContext.LoaiBangLais
                .Where(l => !l.isDeleted)
                .ToListAsync();
        }

        public async Task<LoaiBangLai> GetLoaiBangLaiByIdAsync(Guid loaiBangLaiId)
        {
            return await _dbContext.LoaiBangLais
                .FirstOrDefaultAsync(l => l.Id == loaiBangLaiId && !l.isDeleted);
        }

        public async Task<(LoaiBangLai, List<ChuDe>)> GetChuDeByLoaiBangLaiAsync(Guid loaiBangLaiId)
        {
            var loai = await _dbContext.LoaiBangLais
                .FirstOrDefaultAsync(l => l.Id == loaiBangLaiId && !l.isDeleted);

            var chuDeList = await _dbContext.ChuDes
                .Include(cd => cd.CauHois)
                .Where(cd => !cd.isDeleted &&
                             cd.CauHois.Any(ch => ch.LoaiBangLaiId == loaiBangLaiId &&
                                                  !ch.LoaiBangLai.isDeleted &&
                                                  !ch.ChuDe.isDeleted))
                .Distinct()
                .ToListAsync();

            return (loai, chuDeList);
        }
        public async Task<PageList<LoaiBangLai>> GetPagedLoaiBangLai(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            IQueryable<LoaiBangLai> LoaiBangLaiQuery = _dbContext.LoaiBangLais.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                LoaiBangLaiQuery = LoaiBangLaiQuery.Where(p => p.TenLoai.Contains(search));
            }
            LoaiBangLaiQuery = LoaiBangLaiQuery.Where(x => !x.isDeleted);
            var entityProps = typeof(LoaiBangLai)
            .GetProperties()
            .ToDictionary(p => p.Name.ToLower(), p => p.Name, StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrWhiteSpace(sortCol) && entityProps.ContainsKey(sortCol))
            {
                string actualCol = entityProps[sortCol];
                bool isDescending = sortDir?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false;

                LoaiBangLaiQuery = isDescending
                    ? LoaiBangLaiQuery.OrderByDescending(q => EF.Property<object>(q, actualCol))
                    : LoaiBangLaiQuery.OrderBy(q => EF.Property<object>(q, actualCol));
            }
            else
            {
                LoaiBangLaiQuery = LoaiBangLaiQuery.OrderBy(q => q.Id);
            }
            var LoaiBangLais = await PageList<LoaiBangLai>.CreatePageAsync(LoaiBangLaiQuery, pageNumber, pageSize);
            return LoaiBangLais;
        }
        public async Task<LoaiBangLai> GetLoaiBangLaiById(Guid id)
        {
            return await _dbContext.LoaiBangLais.FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task<List<LoaiBangLai>> GetLoaiBangLaiHasMoPhong()
        {
            return await _dbContext.LoaiBangLais
                .Where(l => !l.isDeleted && l.MoPhongs.Any())
                .ToListAsync();
        }
    }
}