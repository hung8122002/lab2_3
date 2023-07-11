using Lab2_3.Bussiness.DTO;

namespace Lab2_3.Bussiness.IRepository
{
    public interface IProductRepository
    {
        public List<ProductDTO> GetPagingProduct(int CurrentPage, int? cateId, int? min, int? max, int sort, string? name);

        public ProductDTO GetProduct(int ProductId);

        public int GetTotalPage();
    }
}
