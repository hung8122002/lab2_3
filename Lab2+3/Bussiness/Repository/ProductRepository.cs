using AutoMapper;
using Lab2_3.Bussiness.DTO;
using Lab2_3.Bussiness.IRepository;
using Lab2_3.DataAccess.Managers;
using Lab2_3.DataAccess.Models;

namespace Lab2_3.Bussiness.Repository
{
    public class ProductRepository : IProductRepository
    {
        readonly NorthwindContext _context;
        readonly IMapper _mapper;
        readonly ProductManeger productManeger;

        public ProductRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            productManeger = new(_context);
        }
        public List<ProductDTO> GetPagingProduct(int CurrentPage, int? cateId, int? min, int? max, int sort, string? name)
        {
            List<Product> product = productManeger.GetPagingProduct(CurrentPage * 20 - 20, cateId, min, max, sort, name);
            List<ProductDTO> productDTO = _mapper.Map<List<ProductDTO>>(product);
            return productDTO;
        }

        public ProductDTO GetProduct(int ProductId)
        {
            Product product = productManeger.GetProduct(ProductId);
            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);
            return productDTO;
        }

        public int GetTotalPage()
        {
            double totalPage = productManeger.GetTotalPage() / 20.0;
            return (int)Math.Ceiling(totalPage);
        }
    }
}
