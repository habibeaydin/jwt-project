using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IToDoService
    {
        IEnumerable<ToDoItem> GetItemsByUserId(int userId);
        ToDoItem GetById(int id);
        ToDoItem Add(ToDoItem item);
        void Update(int id, ToDoItem item);
        void Delete(int id);
    }
}
