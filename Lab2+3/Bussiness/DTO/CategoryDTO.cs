using Lab2_3.DataAccess.Models;

namespace Lab2_3.Bussiness.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public byte[]? Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
