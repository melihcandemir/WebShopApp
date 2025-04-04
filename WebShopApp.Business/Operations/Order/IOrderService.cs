using WebShopApp.Business.Operations.Order.Dtos;
using WebShopApp.Business.Types;

namespace WebShopApp.Business.Operations.Order
{
    // lifetime belirtmek gerekiyor
    public interface IOrderService
    {
        Task<ServisMessage> AddOrder(AddOrderDto order);
        Task<OrderDto> GetOrder(int id);
        Task<List<OrderDto>> GetAllOrders();
        Task<ServisMessage> DeleteOrder(int id);
        Task<ServisMessage> UpdateOrder(UpdateOrderDto order);
    }
}