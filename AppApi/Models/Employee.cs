namespace AppApi.Models
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Image { get; set; }
    }
}
