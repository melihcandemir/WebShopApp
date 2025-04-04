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

        public async Task<ServisMessage> DeleteProdut(int id)
        {
            var product = _repository.GetById(id);

            if (product is null)
            {
                return new ServisMessage
                {
                    IsSucceed = false,
                    Message = "Silinmek istenen ürün bulunamadı."
                };
            }

            _repository.Delete(id);


            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Ürün silerken bir hata oluştu.");
            }

            return new ServisMessage
            {
                IsSucceed = true,
                Message = "Ürün silindi."
            };
        }

        public async Task<ServisMessage> PriceUpdate(int id, decimal changeBy)
        {
            var product = _repository.GetById(id);

            if (product is null)
            {
                return new ServisMessage
                {
                    IsSucceed = false,
                    Message = "Bu id ye ait ürün bulunamadı."
                };
            }

            product.Price = changeBy;

            _repository.Update(product);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Ürün fiyatı güncellenirken bir hata oluştu.");
            }

            return new ServisMessage
            {
                IsSucceed = true,
                Message = "Ürün güncellendi."
            };
        }

        public async Task<ServisMessage> UpdateProduct(UpdateProductDto product)
        {
            var productEntity = _repository.GetById(product.Id);

            if (productEntity is null)
            {
                return new ServisMessage
                {
                    IsSucceed = false,
                    Message = "Güncellenmek istenen ürün bulunamadı."
                };
            }

            await _unitOfWork.BeginTransaction();

            productEntity.ProductName = product.ProductName;
            productEntity.Price = product.Price;
            productEntity.StockQuantity = product.StockQuantity;

            _repository.Update(productEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Ürün bilgileri güncellenirken bir hata oluştu işlemler geriye alınıyor.");
            }

            return new ServisMessage
            {
                IsSucceed = true,
                Message = "Başarıyla güncellendi."
            };
        }
    }
}