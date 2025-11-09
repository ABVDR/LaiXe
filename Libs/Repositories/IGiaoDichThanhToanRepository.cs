using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IGiaoDichThanhToanRepository
    {
        Task<GiaoDichThanhToan> CreatePendingAsync(long donHangId, string cong, string? maDonCong, CancellationToken ct);
        Task AttachGatewayAsync(long donHangId, string cong, string requestId, string maDonCong, CancellationToken ct = default);
        Task MarkPaidAsync(long donHangId, string cong, string maGiaoDichCuoi, CancellationToken ct);
        Task MarkFailedAsync(long donHangId, string cong, string lyDo, CancellationToken ct);
        Task<GiaoDichThanhToan?> GetByOrderAsync(long donHangId, string cong, CancellationToken ct);
    }
}
