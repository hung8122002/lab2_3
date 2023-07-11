using AutoMapper;
using Lab2_3.Bussiness.DTO;
using Lab2_3.Bussiness.IRepository;
using Lab2_3.DataAccess.Managers;
using Lab2_3.DataAccess.Models;

namespace Lab2_3.Bussiness.Repository
{
    public class OrderRepository : IOrderRepository
    {
        NorthwindContext _context;
        IMapper _mapper;

        public OrderRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddOrder(OrderDTO orderDTO, List<OrderDetailDTO> detailDTOs)
        {
            OrderManager orderManager = new(_context);
            List<OrderDetail> orderDetailsyDTOs = _mapper.Map<List<OrderDetail>>(detailDTOs);
            Order order = _mapper.Map<Order>(orderDTO);
            orderManager.AddOrder(order, orderDetailsyDTOs);
        }
    }
}