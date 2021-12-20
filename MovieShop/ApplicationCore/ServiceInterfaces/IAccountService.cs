using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
     public interface IAccountService
     {
          Task<int> RegisterUser(UserRegisterRequestModel model);
          Task<UserLoginResponseModel> ValidateUser(LoginRequestModel model);
     }
}
