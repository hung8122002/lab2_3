using Lab2_3.Bussiness.DTO;

namespace Lab2_3.Bussiness.IRepository
{
    public interface IOrderRepository
    {
        public void AddOrder(OrderDTO orderDTO, List<OrderDetailDTO> detailDTOs);
    }
}
