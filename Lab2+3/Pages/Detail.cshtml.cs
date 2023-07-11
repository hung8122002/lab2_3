using Lab2_3.Bussiness.DTO;
using Lab2_3.Bussiness.IRepository;
using Lab2_3.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Web;

namespace Lab2_3.Pages
{
    public class ProductModel : PageModel
    {
        private IProductRepository _productRepository;
        public ProductDTO productDTO { get; set; }

        public ProductModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public class Item
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string ImageLink { get; set; }
            public string Price { get; set; }
            public string Number { get; set; }
        }

        public void OnGet(int id)
        {
            productDTO = _productRepository.GetProduct(id);
        }

        public JsonResult OnGetQuantity()
        {
            List<Item> Items = new();
            string cookieValue = Request.Cookies["cart"];
            if (cookieValue != null)
            {
                var cartObject = JsonConvert.DeserializeObject<List<Item>>(HttpUtility.UrlDecode(cookieValue));
                Items = cartObject;
                for (int i = 0; i < cartObject.Count; i++)
                {
                    ProductDTO productDTO = _productRepository.GetProduct(Int32.Parse(Items[i].Id));
                    Items[i].Number = productDTO.UnitsInStock.ToString();
                }
            }
            var result = new { Items };
            return new JsonResult(result);
        }
    }
}
