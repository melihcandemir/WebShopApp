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
                Password = _dataProtector.Protect(user.Password), // encryption will be performed.
                PhoneNumber = user.PhoneNumber,
                UserType = UserType.Customer // Assigned to Customer by default

            };

            _userRepository.Add(UserEntity);

            try
            {
                // if save is successful
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                // If we get an error
                throw new Exception("Kullanıcı eklenirken bir hata oluştu.");
            }


            // if save is successful
            return new ServisMessage
            {
                Message = "Kullanıcı başarıyla eklendi.",
                IsSucceed = true
            };
        }

        public ServisMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());

            if (userEntity == null)
            {
                return new ServisMessage<UserInfoDto>
                {
                    Message = "Kullanıcı bulunamadı.",
                    IsSucceed = false
                };
            }

            var unprotectedPassword = _dataProtector.Unprotect(userEntity.Password);

            if (unprotectedPassword == user.Password)
            {
                return new ServisMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        UserType = userEntity.UserType,
                        PhoneNumber = userEntity.PhoneNumber
                    }
                };
            }
            else
            {
                return new ServisMessage<UserInfoDto>
                {
                    Message = "Kullanıcı adı veya şifre hatalı.",
                    IsSucceed = false
                };
            }
        }
    }
}