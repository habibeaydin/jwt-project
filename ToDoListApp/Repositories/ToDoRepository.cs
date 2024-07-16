using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Repositories
{
    public class ToDoRepository : IToDoRepository 
    {
        private readonly ToDoContext _context;
        public ToDoRepository(ToDoContext context)
        {
            _context = context;
        }

        public IEnumerable<ToDoItem> GetItemsByUserId(int userId) 
        {
            return _context.ToDoItems
                   .Where(item => item.UserId == userId)
                   .ToList();
        } //user bu olan tüm to do ları getir.

        public ToDoItem GetById(int id) => _context.ToDoItems.Find(id)!;

        public ToDoItem Add(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Update(ToDoItem item)
        {
            _context.Update(item);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item != null)
            {
                _context.ToDoItems.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
