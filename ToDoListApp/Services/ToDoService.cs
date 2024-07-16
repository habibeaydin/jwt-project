using ToDoListApp.Models;
using ToDoListApp.Repositories;

namespace ToDoListApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ToDoItem> GetItemsByUserId(int userId)
        {
        // TODO: JWT token içindeki id bilgisine erişmek. 
            return _repository.GetItemsByUserId(userId);
        }

        public ToDoItem GetById(int id) 
        {            
            var item = _repository.GetById(id);
            if (item == null) 
            { 
                throw new Exception("Not Found");
            }                        
            return _repository.GetById(id);
        } 

        public ToDoItem Add(ToDoItem item) => _repository.Add(item);

        public void Update(int id, ToDoItem item)
        {
            try
            {
                if (id != item.Id)
                {
                    throw new Exception("!!!");
                }
                var existingItem = _repository.GetById(id);
                if (existingItem == null)
                {
                    throw new Exception("Not Found");
                }

                existingItem.Title = item.Title;
                existingItem.Description = item.Description;
                existingItem.IsCompleted = item.IsCompleted;
                existingItem.UserId = item.UserId;
                existingItem.User = item.User;

                _repository.Update(existingItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            var existingItem = GetById(id);
            if (existingItem == null)
            {
                throw new Exception("Not Found");
            }
            _repository.Delete(id);
        }
    }
}
