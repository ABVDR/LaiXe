using Libs.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public sealed class GiaoDichThanhToanRepository : IGiaoDichThanhToanRepository
    {
        private readonly ApplicationDbContext _db;
        public GiaoDichThanhToanRepository(ApplicationDbContext db) => _db = db;

        public async Task<GiaoDichThanhToan> CreatePendingAsync(long donHangId, string cong, string? maDonCong, CancellationToken ct)
        {
            var exists = await _db.GiaoDichThanhToans
                .FirstOrDefaultAsync(x => x.DonHangId == donHangId && x.CongThanhToan == cong, ct);
            if (exists != null) return exists;

            var tx = new GiaoDichThanhToan
            {
                DonHangId = donHangId,
                CongThanhToan = cong,
                MaDonCong = maDonCong,
                TrangThai = TrangThaiThanhToan.ChoXuLy,
                NgayTao = DateTimeOffset.UtcNow
            };
            _db.GiaoDichThanhToans.Add(tx);
            await _db.SaveChangesAsync(ct);
            return tx;
        }

        public async Task AttachGatewayAsync(long donHangId, string cong, string requestId, string maDonCong, CancellationToken ct = default)
        {
            var tx = await _db.GiaoDichThanhToans
                         .FirstOrDefaultAsync(x => x.DonHangId == donHangId && x.CongThanhToan == cong, ct)
                     ?? new GiaoDichThanhToan
                     {
                         DonHangId = donHangId,
                         CongThanhToan = cong,
                         TrangThai = TrangThaiThanhToan.ChoXuLy,
                         NgayTao = DateTimeOffset.UtcNow
                     };

            tx.MaDonCong = maDonCong;
            tx.NgayCapNhat = DateTimeOffset.UtcNow;

            _db.Update(tx);
            await _db.SaveChangesAsync(ct);
        }

        public async Task MarkPaidAsync(long donHangId, string cong, string maGiaoDichCuoi, CancellationToken ct)
        {
            // Idempotent: nếu đã có transactionId này ở trạng thái thành công thì bỏ qua
            var existedSameTxn = await _db.GiaoDichThanhToans
                .AnyAsync(x => x.MaGiaoDichCuoi == maGiaoDichCuoi && x.TrangThai == TrangThaiThanhToan.ThanhCong, ct);
            if (existedSameTxn) return;

            var tx = await _db.GiaoDichThanhToans
                .FirstAsync(x => x.DonHangId == donHangId && x.CongThanhToan == cong, ct);

            tx.TrangThai = TrangThaiThanhToan.ThanhCong;
            tx.MaGiaoDichCuoi = maGiaoDichCuoi;
            tx.NgayCapNhat = DateTimeOffset.UtcNow;

            // cập nhật đơn hàng (nếu có DbSet DonHang)
            var dh = await _db.DonHangs.FirstOrDefaultAsync(d => d.Id == donHangId, ct);
            if (dh != null)
            {
                dh.TrangThai = TrangThaiThanhToan.ThanhCong;
            }

            await _db.SaveChangesAsync(ct);
        }

        public async Task MarkFailedAsync(long donHangId, string cong, string lyDo, CancellationToken ct)
        {
            var tx = await _db.GiaoDichThanhToans
                         .FirstOrDefaultAsync(x => x.DonHangId == donHangId && x.CongThanhToan == cong, ct)
                     ?? new GiaoDichThanhToan { DonHangId = donHangId, CongThanhToan = cong, NgayTao = DateTimeOffset.UtcNow };

            tx.TrangThai = TrangThaiThanhToan.ThatBai;
            tx.ThongBaoLoi = lyDo;
            tx.NgayCapNhat = DateTimeOffset.UtcNow;

            _db.Update(tx);

            var dh = await _db.DonHangs.FirstOrDefaultAsync(d => d.Id == donHangId, ct);
            if (dh != null)
            {
                dh.TrangThai = TrangThaiThanhToan.ThatBai;
            }

            await _db.SaveChangesAsync(ct);
        }

        public Task<GiaoDichThanhToan?> GetByOrderAsync(long donHangId, string cong, CancellationToken ct)
            => _db.GiaoDichThanhToans.FirstOrDefaultAsync(x => x.DonHangId == donHangId && x.CongThanhToan == cong, ct);
    }
}
