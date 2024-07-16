using ToDoListApp.Models;

namespace ToDoListApp.Repositories
{
    public interface IToDoRepository
    {
        IEnumerable<ToDoItem> GetItemsByUserId(int userId);
        ToDoItem GetById(int id);
        ToDoItem Add(ToDoItem item);
        void Update(ToDoItem item);
        void Delete(int id);
    }
}
