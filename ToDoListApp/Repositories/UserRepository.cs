using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoContext _context;

        public UserRepository(ToDoContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public User GetById(int id) => _context.Users.Find(id)!;

        public void Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public async Task<User> GetUserByUsernameAndPassw(string username, string passw)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == passw);
        }
    }
}
