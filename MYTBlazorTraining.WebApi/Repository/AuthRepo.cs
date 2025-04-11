using MYTBlazorTraining.WebApi.Models.Login;
using MYTBlazorTraining.WebApi.Models.Register;
using MYTBlazorTraining.WebApi.Services;

namespace MYTBlazorTraining.WebApi.Repository
{
    public class AuthRepo : IAuth
    {
        public Task<UserLoginResponseModel> UserLoginAsync(UserLoginModel userLoginModel)
        {
            throw new NotImplementedException();
        }

        public Task<UserRegisterResponseModel> UserRegisterAsync(UserRegisterModel userRegisterModel)
        {
            throw new NotImplementedException();
        }
    }
}
