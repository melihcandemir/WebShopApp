using WebShopApp.Business.DataProtection;
using WebShopApp.Business.Operations.User.Dtos;
using WebShopApp.Business.Types;
using WebShopApp.Data.Entities;
using WebShopApp.Data.Enums;
using WebShopApp.Data.Repositories;
using WebShopApp.Data.UnitOfWork;

namespace WebShopApp.Business.Operations.User
{

    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _dataProtector;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _dataProtector = protector;
        }


        public async Task<ServisMessage> AddUser(AddUserDto user)
        {

            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());

            if (hasMail.Any())
            {
                return new ServisMessage
                {
                    Message = "Bu mail adresi zaten kullanılmaktadır.",
                    IsSucceed = false
                };
            }

            var UserEntity = new UserEntity
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = _dataProtector.Protect(user.Password), // şifreleme yapılacak.
                PhoneNumber = user.PhoneNumber,
                UserType = UserType.Customer // Varsayılan olarak Customer atanıyor

            };

            _userRepository.Add(UserEntity);

            try
            {
                // save başarılı ise
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                // hata alırsak
                throw new Exception("Kullanıcı eklenirken bir hata oluştu.");
            }


            // save başarılı ise
            return new ServisMessage
            {
                Message = "Kullanıcı başarıyla eklendi.",
                IsSucceed = true
            };
        }
    }
}