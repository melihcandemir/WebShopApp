using WebShopApp.Business.Operations.Product.Dtos;
using WebShopApp.Business.Types;

namespace WebShopApp.Business.Operations.Product
{
    // lifetime belirtmek gerekiyor
    public interface IProductService
    {
        Task<ServisMessage> AddProduct(AddProductDto product);
    }
}