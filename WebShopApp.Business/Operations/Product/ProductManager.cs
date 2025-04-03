using WebShopApp.Business.Operations.Product.Dtos;
using WebShopApp.Business.Types;
using WebShopApp.Data.Entities;
using WebShopApp.Data.Repositories;
using WebShopApp.Data.UnitOfWork;

namespace WebShopApp.Business.Operations.Product
{

    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductEntity> _repository;

        public ProductManager(IUnitOfWork unitOfWork, IRepository<ProductEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ServisMessage> AddProduct(AddProductDto product)
        {
            var hasProduct = _repository.GetAll(x => x.ProductName.ToLower() == product.ProductName.ToLower()).Any();

            if (hasProduct)
            {
                return new ServisMessage
                {
                    IsSucceed = false,
                    Message = "Bu ürün bulunuyor."
                };
            }

            var productEntity = new ProductEntity
            {
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            _repository.Add(productEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Ürün kaydı sırasında bir hata oluştu.");
            }

            return new ServisMessage
            {
                IsSucceed = true,
                Message = "Ürün eklendi"
            };
        }
    }
}