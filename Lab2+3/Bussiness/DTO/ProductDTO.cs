using Lab2_3.DataAccess.Models;

namespace Lab2_3.Bussiness.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? ImageLink { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public virtual CategoryDTO? Category { get; set; }
    }
}
