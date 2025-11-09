using System.Text.Json.Serialization;

namespace Libs.Entity
{
    public class ChiTietBaiThi
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid BaiThiId { get; set; }           
        public Guid CauHoiId { get; set; }
        [JsonIgnore]
        public virtual BaiThi BaiThi { get; set; }
        public virtual CauHoi CauHoi { get; set; }
    }
}
