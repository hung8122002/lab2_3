using Lab2_3.DataAccess.Models;

namespace Lab2_3.DataAccess.Managers
{
    public class CategoryManager
    {
        NorthwindContext _context;

        public CategoryManager(NorthwindContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            List<Category> product = _context.Categories.ToList();
            return product;
        }
    }
}
