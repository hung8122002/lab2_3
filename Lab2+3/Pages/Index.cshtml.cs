using Lab2_3.Bussiness.DTO;
using Lab2_3.Bussiness.IRepository;
using Lab2_3.Bussiness.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query;

namespace Lab2_3.Pages
{
    public class IndexModel : PageModel
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        public List<ProductDTO> productDTOs { get; set; }
        public List<CategoryDTO> categoryDTOs { get; set; }
        public int? indexChecked { get; set; }
        public int? min { get; set; }
        public int? max { get; set; }
        public int sort { get; set; }
        public int totalPage { get; set; }
        public int currentPage { get; set; } = 1;
        public string? name { get; set; }

        public IndexModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public void OnGet()
        {
            GetData();
        }

        public void OnPost(int pageIndex, int CateId, string reset, int? minPrice, int? maxPrice, int sort, string? name)
        {
            currentPage = pageIndex;
            if(pageIndex == 0)
            {
                currentPage = 1;
            }
            this.sort = sort;
            indexChecked = CateId;
            min = minPrice;
            max = maxPrice;
            if (reset == "resetFilter")
            {
                indexChecked = 0;
            }
            if (reset == "resetPrice")
            {
                min = 0; max = 0;
            }
            this.name = name;
            GetData();
        }

        public void GetData()
        {
            productDTOs = _productRepository.GetPagingProduct(currentPage, indexChecked, min, max, sort, name);
            totalPage = _productRepository.GetTotalPage();
            categoryDTOs = _categoryRepository.GetCategories();
        }
    }
}