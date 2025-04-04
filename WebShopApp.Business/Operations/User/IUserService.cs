using WebShopApp.Business.Operations.User.Dtos;
using WebShopApp.Business.Types;

namespace WebShopApp.Business.Operations.User
{
    // lifetime must be specified
    public interface IUserService
    {
        Task<ServisMessage> AddUser(AddUserDto user);

        ServisMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}