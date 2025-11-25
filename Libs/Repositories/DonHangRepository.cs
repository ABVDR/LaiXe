using Libs.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IDonHangRepository
    {
        Task<DonHang?> GetAsync(long id, CancellationToken ct);
    }
    public class DonHangRepository : IDonHangRepository
    {
        private readonly ApplicationDbContext _db;
        public DonHangRepository(ApplicationDbContext db) => _db = db;

        public Task<DonHang?> GetAsync(long id, CancellationToken ct)
            => _db.Set<DonHang>().FirstOrDefaultAsync(o => o.Id == id, ct);
    }
}
