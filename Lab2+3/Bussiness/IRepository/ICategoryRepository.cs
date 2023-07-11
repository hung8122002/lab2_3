using Lab2_3.Bussiness.DTO;

namespace Lab2_3.Bussiness.IRepository
{
    public interface ICategoryRepository
    {
        public List<CategoryDTO> GetCategories();
    }
}
