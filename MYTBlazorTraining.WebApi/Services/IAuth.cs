using MYTBlazorTraining.WebApi.Models.Login;
using MYTBlazorTraining.WebApi.Models.Register;

namespace MYTBlazorTraining.WebApi.Services
{
    public interface IAuth
    {
        Task<UserLoginResponseModel> UserLoginAsync(UserLoginModel userLoginModel);
        Task<UserRegisterResponseModel> UserRegisterAsync(UserRegisterModel userRegisterModel);
    }
}
