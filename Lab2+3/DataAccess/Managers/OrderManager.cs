using Lab2_3.DataAccess.Models;

namespace Lab2_3.DataAccess.Managers
{
    public class OrderManager
    {
        NorthwindContext _context;

        public OrderManager(NorthwindContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order, List<OrderDetail> details)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var item in details)
            {
                Product p = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductId);
                p.UnitsInStock -= item.Quantity;
                p.UnitsOnOrder += item.Quantity;
                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Discount = 0,
                });
            }
            _context.SaveChanges();
        }
    }
}
