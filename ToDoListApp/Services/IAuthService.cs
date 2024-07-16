using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IAuthService
    {
        public Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);
    }
}
