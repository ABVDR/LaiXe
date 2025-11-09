namespace Libs.Entity
{
    public class BaiSaHinh
    {
        public bool isDeleted { get; set; } = false;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenBai { get; set; }
        public int Order { get; set; }
        public string NoiDung { get; set; }
        public Guid LoaiBangLaiId { get; set; }
        public LoaiBangLai? LoaiBangLai { get; set; }
    }
}
