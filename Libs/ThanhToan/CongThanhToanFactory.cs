using Libs.Entity;
using Libs.ThanhToan.Abstractions;
using Libs.ThanhToan.Providers;
using System;

namespace Libs.ThanhToan
{
    
    public sealed class CongThanhToanFactory : ICongThanhToanFactory
    {
        private readonly CongMoMo _momo;
        private readonly CongPayPal _paypal;

        public CongThanhToanFactory(CongMoMo momo, CongPayPal paypal)
        {
            _momo = momo;
            _paypal = paypal;
        }

        public ICongThanhToan Resolve(PhuongThucThanhToan phuongThuc) => phuongThuc switch
        {
            PhuongThucThanhToan.MoMo => _momo,
            PhuongThucThanhToan.PayPal => _paypal,
            _ => throw new NotSupportedException($"Phương thức thanh toán '{phuongThuc}' chưa được hỗ trợ.")
        };
    }
}
