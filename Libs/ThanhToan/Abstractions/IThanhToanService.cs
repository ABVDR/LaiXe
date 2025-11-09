using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PaymentMethod = Libs.Entity.PhuongThucThanhToan;

namespace Libs.ThanhToan.Abstractions
{
    public interface IThanhToanService
    {
        Task<TaoPhienThanhToanResult> TaoThanhToanAsync(TaoPhienThanhToanRequest req, CancellationToken ct);
        Task DanhDauThanhCongAsync(long donHangId, string cong, string maGiaoDichCuoi, CancellationToken ct);
        Task DanhDauThatBaiAsync(long donHangId, string cong, string lyDo, CancellationToken ct);
    }

    public interface ICongThanhToanFactory
    {
        ICongThanhToan Resolve(PaymentMethod phuongThuc);
    }
}