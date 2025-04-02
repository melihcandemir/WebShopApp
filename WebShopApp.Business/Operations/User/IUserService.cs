using WebShopApp.Business.Operations.User.Dtos;
using WebShopApp.Business.Types;

namespace WebShopApp.Business.Operations.User
{
    // lifetime belirtmek gerekiyor
    public interface IUserService
    {
        Task<ServisMessage> AddUser(AddUserDto user); // asyn çünkü unit of work kullanılacak.
    }
}