namespace Libs.Entity
{
    public class CauHoiSai
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; }

        public Guid CauHoiId { get; set; } 

        public DateTime NgaySai { get; set; }

        // Navigation
        public virtual CauHoi CauHoi { get; set; }
    }
}
