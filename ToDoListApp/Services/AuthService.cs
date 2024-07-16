using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new();

            User user = null;

            try
            {
                user = await _userService.GetUserByUsernameAndPassw(request.Username, request.Password);
            }
            catch (Exception)
            {
                response.AuthenticateResult = false;
            }

            var generatedTokenInformation = await _tokenService.GenerateToken(new GenerateTokenRequest { 
                Username = request.Username,
                Id = user.Id
            });
            response.AuthenticateResult = true;
            response.AuthToken = generatedTokenInformation.Token;
            response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;

            return response;
        }
    }
}
