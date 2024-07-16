using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;
using ToDoListApp.Repositories;

namespace ToDoListApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User user) => _userRepository.Add(user);    

        public void Delete(int id)
        {
            var usr = _userRepository.GetById(id);
            if (usr == null)
            {
                throw new Exception("Not Found");
            }
            _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAll() => _userRepository.GetAll();

        public User GetById(int id)
        {
            var usr = _userRepository.GetById(id);
            if(usr == null)
            {
                throw new Exception("Not Found");
            }
            return _userRepository.GetById(id);
        }

        public void Update(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    throw new Exception("!!!");
                }
                var existingUser = _userRepository.GetById(id);
                if (existingUser == null)
                {
                    throw new Exception("Not Found");
                }

                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;

                _userRepository.Update(existingUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByUsernameAndPassw(string username, string passw)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passw))
            {
                throw new Exception("Username and Password are empty");
            }

            var user = await _userRepository.GetUserByUsernameAndPassw(username, passw);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            return user;
        }
    }
}
