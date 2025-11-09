using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface ITinhNangMoKhoaRepository
    {
        Task<TinhNangMoKhoa?> GetByUserAndFeatureAsync(string userId, string tenTinhNang, CancellationToken ct = default);
        Task<TinhNangMoKhoa> CreateAsync(string userId, string tenTinhNang, long donHangId, decimal soTien, CancellationToken ct = default);
        Task ActivateAsync(long donHangId, CancellationToken ct = default);
    }
}
