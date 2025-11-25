using System;
using System.Threading;
using System.Threading.Tasks;

using Libs.ThanhToan.Abstractions;
using Libs.Repositories;

// Alias repo tiếng Việt
using IOrderRepository = Libs.Repositories.IDonHangRepository;
using IPaymentTransactionRepository = Libs.Repositories.IGiaoDichThanhToanRepository;

// Alias enum tiếng Việt
using PaymentMethod = Libs.Entity.PhuongThucThanhToan;
using PaymentStatus = Libs.Entity.TrangThaiThanhToan;

namespace Libs.ThanhToan
{
    public sealed class ThanhToanService : IThanhToanService
    {
        private readonly ICongThanhToanFactory _factory;
        private readonly IOrderRepository _orders;
        private readonly IPaymentTransactionRepository _txRepo;

        public ThanhToanService(
            ICongThanhToanFactory factory,
            IOrderRepository orders,
            IPaymentTransactionRepository txRepo)
        {
            _factory = factory;
            _orders = orders;
            _txRepo = txRepo;
        }

        public async Task<TaoPhienThanhToanResult> TaoThanhToanAsync(TaoPhienThanhToanRequest req, CancellationToken ct)
        {
            // Chỉ dùng cho redirect-based gateways (hiện tại: MoMo)
            if (req.PhuongThuc != PaymentMethod.MoMo)
            {
                return new(false, null, null, "Phương thức này không dùng redirect-flow.");
            }

            var donHang = await _orders.GetAsync(req.DonHangId, ct)
                          ?? throw new InvalidOperationException("Không tìm thấy đơn hàng");

            if (donHang.TrangThai == PaymentStatus.ThanhCong)
                return new(true, null, null, "Đơn hàng đã thanh toán");

            var cong = _factory.Resolve(req.PhuongThuc);
            return await cong.TaoPhienAsync(req.DonHangId, req.ReturnUrl, ct);
        }

        public Task DanhDauThanhCongAsync(long donHangId, string cong, string maGiaoDichCuoi, CancellationToken ct)
            => _txRepo.MarkPaidAsync(donHangId, cong, maGiaoDichCuoi, ct);

        public Task DanhDauThatBaiAsync(long donHangId, string cong, string lyDo, CancellationToken ct)
            => _txRepo.MarkFailedAsync(donHangId, cong, lyDo, ct);
    }
}