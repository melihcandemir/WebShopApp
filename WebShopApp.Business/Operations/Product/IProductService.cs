using WebShopApp.Business.Operations.User.Dtos;
using WebShopApp.Business.Types;

namespace WebShopApp.Business.Operations.User
{
    // lifetime belirtmek gerekiyor
    public interface IProductService
    {
        Task<ServisMessage> AddProduct(AddProductDto product);
    }
}