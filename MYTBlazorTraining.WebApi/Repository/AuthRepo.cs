using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MYTBlazorTraining.WebApi.Db;
using MYTBlazorTraining.WebApi.Models.Login;
using MYTBlazorTraining.WebApi.Models.Register;
using MYTBlazorTraining.WebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MYTBlazorTraining.WebApi.Repository
{
    public class AuthRepo : IAuth
    {
        private readonly AppDbContext _contenxt;
        private readonly IConfiguration _configuration;

        public AuthRepo(AppDbContext contenxt, IConfiguration configuration)
        {
            _contenxt = contenxt;
            _configuration = configuration;
        }

        public async Task<UserLoginResponseModel> UserLoginAsync(UserLoginModel userLoginModel)
        {
            var getUser = await FindUserByEmail(userLoginModel.Email!);
            if (getUser is null)
                return new UserLoginResponseModel(false, "User Not Found");
            var checkPassword = BCrypt.Net.BCrypt.Verify(userLoginModel.Password, getUser.Password);
            if (!checkPassword)
                return new UserLoginResponseModel(false, "Credentials are not valid");
            return new UserLoginResponseModel(true, "Login Successfully", GenerateJWTToken(getUser));
        }

        public async Task<UserRegisterResponseModel> UserRegisterAsync(UserRegisterModel userRegisterModel)
        {
            var getUser = await FindUserByEmail(userRegisterModel.Email!);
            if (getUser is not null) return new UserRegisterResponseModel(false, "User Already Exist");
            var user = new TblUser
            {
                Name = userRegisterModel.Name,
                Email = userRegisterModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userRegisterModel.Password)
            };
            await _contenxt.Users.AddAsync(user);
            var result = await _contenxt.SaveChangesAsync();
            if (result is 0) return new UserRegisterResponseModel(false, "User Not Created");
            return new UserRegisterResponseModel(true, "User Created Successfully");
        }

        private async Task<TblUser?> FindUserByEmail(string email) =>
            await _contenxt.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);

        private string GenerateJWTToken(TblUser user)
        {
            var key = _configuration["Jwt:Key"]; // get from appsettings
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim("Id", user.Id.ToString()),
        new Claim("Name", user.Name!),
        new Claim("Email", user.Email!)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

