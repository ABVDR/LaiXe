using Libs.Entity;
using Libs.ThanhToan.Abstractions;
using Libs.ThanhToan.Providers;
using System;

namespace Libs.ThanhToan
{
    public sealed class CongThanhToanFactory : ICongThanhToanFactory
    {
        private readonly CongMoMo _momo;

        public CongThanhToanFactory(CongMoMo momo)
        {
            _momo = momo;
        }

        public ICongThanhToan Resolve(PhuongThucThanhToan phuongThuc) => phuongThuc switch
        {
            PhuongThucThanhToan.MoMo => _momo,
            _ => throw new NotSupportedException(
                    $"Phương thức thanh toán '{phuongThuc}' hiện chỉ hỗ trợ MoMo cho redirect-flow.")
        };
    }
}
