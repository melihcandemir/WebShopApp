using WebShopApp.Business.Operations.Order.Dtos;
using WebShopApp.Business.Types;

namespace WebShopApp.Business.Operations.Order
{
    // lifetime belirtmek gerekiyor
    public interface IOrderService
    {
        Task<ServisMessage> AddOrder(AddOrderDto order);
    }
}