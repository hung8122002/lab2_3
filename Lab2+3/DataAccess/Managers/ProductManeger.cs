using Lab2_3.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2_3.DataAccess.Managers
{
    public class ProductManeger
    {
        readonly NorthwindContext _context;

        List<Product> Products { get; set; }

        public ProductManeger(NorthwindContext context)
        {
            _context = context;
        }

        public List<Product> GetPagingProduct(int StartIndex, int? cateId, int? min, int? max, int sort, string? name)
        {
            List<Product> products = _context.Products.ToList();
            if (cateId > 0)
            {
                products = products.Where(p => p.CategoryId == cateId).ToList();
            }
            if (min > 0)
            {
                products = products.Where(p => p.UnitPrice >= min).ToList();
            }
            if (max > 0)
            {
                products = products.Where(p => p.UnitPrice <= max).ToList();
            }
            if (sort == 1)
            {
                products = products.OrderBy(p => p.UnitPrice).ToList();
            }
            if (sort == 2)
            {
                products = products.OrderByDescending(p => p.UnitPrice).ToList();
            }
            if(name != null && name.Trim().Length > 0)
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToList();
            }
            Products = products;
            products = products.Skip(StartIndex).Take(20).ToList();
            return products;
        }

        public Product GetProduct(int Id)
        {
            Product product = _context.Products.Include(c => c.Category).FirstOrDefault(p => p.ProductId == Id);
            return product;
        }

        public int GetTotalPage()
        {
            return Products.Count;
        }
    }
}
