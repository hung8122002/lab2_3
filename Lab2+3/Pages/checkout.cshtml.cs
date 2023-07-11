using Lab2_3.Bussiness.DTO;
using Lab2_3.Bussiness.IRepository;
using Lab2_3.Bussiness.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Web;

namespace Lab2_3.Pages
{
    public class checkoutModel : PageModel
    {
        private IOrderRepository _orderRepository;

        public checkoutModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public class Item
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string ImageLink { get; set; }
            public string Price { get; set; }
            public string Number { get; set; }
        }

        public List<Item> Items { get; set; }
        public double Total { get; set; }
        public bool orderSuccess { get; set; }
        public void OnGet()
        {
            LoadData();
        }

        public void LoadData()
        {
            orderSuccess = false;
            string cookieValue = Request.Cookies["cart"];
            if (cookieValue != null)
            {
                var cartObject = JsonConvert.DeserializeObject<List<Item>>(HttpUtility.UrlDecode(cookieValue));
                Items = cartObject;
                foreach (var item in cartObject)
                {
                    Total += Int32.Parse(item.Number) * Double.Parse(item.Price[1..]);
                }
            }
        }

        public void OnPost(string name, string address)
        {
            LoadData();
            OrderDTO orderDTO = new()
            {
                ShipName = name,
                ShipAddress = address
            };
            List<OrderDetailDTO> orderDTOList = new();
            foreach (var item in Items)
            {
                orderDTOList.Add(new OrderDetailDTO()
                {
                    ProductId = Int32.Parse(item.Id),
                    Quantity = short.Parse(item.Number),
                    UnitPrice = decimal.Parse(item.Price[1..]),
                });
            }
            _orderRepository.AddOrder(orderDTO, orderDTOList);
            orderSuccess = true;
        }
    }
}
