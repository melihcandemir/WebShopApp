using Microsoft.EntityFrameworkCore;
using WebShopApp.Business.Operations.Order;
using WebShopApp.Business.Operations.Order.Dtos;
using WebShopApp.Business.Types;
using WebShopApp.Data.Entities;
using WebShopApp.Data.Repositories;
using WebShopApp.Data.UnitOfWork;

namespace WebShopApp.Business.Operations.Order
{

    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<OrderProductEntity> _orderProductRepository;

        public OrderManager(IUnitOfWork unitOfWork, IRepository<OrderEntity> repository, IRepository<OrderProductEntity> orderProductRepository, IRepository<ProductEntity> productRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = repository;
            _orderProductRepository = orderProductRepository;
            _productRepository = productRepository;
        }

        public async Task<ServisMessage> AddOrder(AddOrderDto order)
        {
            // save işlemi başlar sorun olursa buraya geri döner
            await _unitOfWork.BeginTransaction();

            decimal totalAmount = 0;
            // sipariş fiyat toplamı
            foreach (var productId in order.ProductIds)
            {
                var hasProduct = _productRepository.GetById(productId);
                if (hasProduct == null)
                {
                    await _unitOfWork.RollBackTransaction();
                    throw new Exception($"Bu Id: {productId} de ürün bulunamadı.");
                }
                totalAmount += hasProduct.Price;
            }


            var orderEntity = new OrderEntity
            {
                CustomerId = order.CustomerId,
                TotalAmount = totalAmount
            };

            _orderRepository.Add(orderEntity);


            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Sipariş kaydı sırasında bir sorunla karşılaşıldı.");
            }

            foreach (var productId in order.ProductIds)
            {
                var orderProduct = new OrderProductEntity
                {
                    OrderId = orderEntity.Id,
                    ProductId = productId,
                    Quentity = order.Quentity
                };

                _orderProductRepository.Add(orderProduct);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Sipariş ürünleri eklenirken bir hata ile karşılaşıldı.");
            }

            return new ServisMessage
            {
                IsSucceed = true,
                Message = "Sipariş ürünleri eklendi"
            };
        }

        public async Task<ServisMessage> DeleteOrder(int id)
        {
            var order = _orderRepository.GetById(id);

            if (order is null)
            {
                return new ServisMessage
                {
                    IsSucceed = false,
                    Message = "Silinmek istenen ürün bulunamadı."
                };
            }

            _orderRepository.Delete(id);


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

        public async Task<List<OrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAll().Select(x => new OrderDto
            {
                Id = x.Id,
                CustomerName = x.User.FirstName + " " + x.User.LastName,
                CreatedDate = x.CreatedDate,
                OrderDate = x.ModifiedDate,
                TotalAmount = x.TotalAmount,
                Products = x.OrderProducts.Select(p => new OrderProductDto
                {
                    Id = p.Id,
                    Title = p.Product.ProductName
                }).ToList()
            }).ToListAsync();

            return orders;
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            var order = await _orderRepository.GetAll(x => x.Id == id).Select(x => new OrderDto
            {
                Id = x.Id,
                CustomerName = x.User.FirstName + " " + x.User.LastName,
                CreatedDate = x.CreatedDate,
                OrderDate = x.ModifiedDate,
                TotalAmount = x.TotalAmount,
                Products = x.OrderProducts.Select(p => new OrderProductDto
                {
                    Id = p.Id,
                    Title = p.Product.ProductName
                }).ToList()
            }).FirstOrDefaultAsync();

            return order;
        }

        public async Task<ServisMessage> UpdateOrder(UpdateOrderDto order)
        {
            var orderEntity = _orderRepository.GetById(order.Id);

            if (orderEntity is null)
            {
                return new ServisMessage
                {
                    IsSucceed = false,
                    Message = "Güncellenmek istenen sipariş bulunamadı."
                };
            }

            await _unitOfWork.BeginTransaction();

            orderEntity.CustomerId = order.CustomerId;

            _orderRepository.Update(orderEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Sipariş bilgileri güncellenirken bir hata ile karşılaşıldı.");
            }

            // sipariş ürünleri güncelleniyor
            var orderProducts = _orderProductRepository.GetAll(x => x.OrderId == order.Id).ToList();

            foreach (var orderProduct in orderProducts)
            {
                _orderProductRepository.Delete(orderProduct, false);
            }

            decimal totalAmount = 0;
            foreach (var productId in order.ProductIds)
            {
                var orderProduct = new OrderProductEntity
                {
                    OrderId = orderEntity.Id,
                    ProductId = productId,
                    Quentity = order.Quentity
                };

                var hasProduct = _productRepository.GetById(productId);
                if (hasProduct == null)
                {
                    await _unitOfWork.RollBackTransaction();
                    throw new Exception($"Bu Id: {productId} de ürün bulunamadı.");
                }
                totalAmount += hasProduct.Price;

                _orderProductRepository.Add(orderProduct);
            }

            orderEntity.TotalAmount = totalAmount;
            _orderRepository.Update(orderEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Sipariş bilgileri güncellenirken bir hata oluştu işlemler geriye alınıyor.");
            }

            return new ServisMessage
            {
                IsSucceed = true,
                Message = "Başarıyla güncellendi."
            };
        }
    }
}