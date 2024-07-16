using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(int id, User user);
        void Delete(int id);
        Task<User> GetUserByUsernameAndPassw(string username, string passw);
    }
}
