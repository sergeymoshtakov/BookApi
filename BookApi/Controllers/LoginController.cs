using BookApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(String username, String password)
        {
            UserModel login = new UserModel()
            {
                Username = username,
                EmailAddress = password
            };
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            // Генеруємо випадковий ключ довжиною 256 біт (32 байти)
            var key = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(key);
            }

            // Створюємо об'єкт SymmetricSecurityKey на основі згенерованого ключа
            var securityKey = new SymmetricSecurityKey(key);

            // Створюємо підпис для токена на основі ключа
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Створюємо JWT токен з підписом
            var token = new JwtSecurityToken(
                issuer: _config["JWTSettings:Issuer"],
                audience: _config["JWTSettings:Audience"],
                claims: null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            // Повертаємо строкове представлення токена
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            if (login.Username == "Sergey")
            {
                user = new UserModel { Username = "Sergey Moshtakov", EmailAddress = "moshtakov.s@gmail.com" };
            }
            return user;
        }
    }
}
