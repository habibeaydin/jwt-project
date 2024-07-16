using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            //SymmetricSecurityKey, JWT'nin imzalanmasında kullanılacak simetrik güvenlik anahtarını temsil eder. Bu anahtar, yapılandırma dosyasındaki Jwt:Secret değerinden alınır.
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]));

            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: _configuration["Jwt:ValidIssuer"],  //Token'ı oluşturan taraf.
                    audience: _configuration["Jwt:ValidAudience"],  //Token'ın geçerli olduğu taraf
                    claims: new List<Claim> {   // Token'a eklenen kullanıcı bilgileri.
                    new Claim("userName", request.Username),
                    new Claim("id", request.Id.ToString())
                    },
                    notBefore: dateTimeNow, //Token'ın geçerli olmaya başlayacağı zaman.
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(60)),    //Token'ın süresinin dolacağı zaman. 
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256) //Token'ı imzalamak için kullanılan imzalama bilgileri. Simetrik güvenlik anahtarı ve HMAC SHA256 algoritması kullanılır.
                );

            return Task.FromResult(new GenerateTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),  //oluşturulan JWT'yi string formatında döner.
                TokenExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(60))
            });
        }
    }
}
