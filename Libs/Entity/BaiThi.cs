namespace Libs.Entity
{
    // Mở rộng model BaiThi với các trường mới
    public partial class BaiThi
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenBaiThi { get; set; }


        // Navigation properties
        public virtual ICollection<ChiTietBaiThi> ChiTietBaiThis { get; set; } = [];
    }
}