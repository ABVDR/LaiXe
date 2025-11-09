using Libs.Entity;
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
}
