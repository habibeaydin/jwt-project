using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface ITokenService
    {
        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request);
    }
}
