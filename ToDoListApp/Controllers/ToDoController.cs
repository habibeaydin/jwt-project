using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Extensions;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [Route("[controller]")] //uses the attribute to define routes
    [ApiController] //enables automatic API behaviors
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly IToDoService _toDoService; //to communicate with the service class that does the business logic

        public ToDoController(IToDoService toDoService) //dependency injection
        {
            _toDoService = toDoService;
        }

        [HttpGet]   
        public ActionResult<IEnumerable<ToDoItem>> GetItemsByUserId()
        {
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var items = _toDoService.GetItemsByUserId(userId.Value);
            return Ok(items);
        }

        [HttpGet("{id:int}")]   
        public ActionResult<ToDoItem> GetById([FromRoute(Name = "id")] int id)
        {
            var item = _toDoService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost] 
        public ActionResult Add([FromBody] ToDoItem item)
        {
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            item.UserId = userId.Value;
            var newItem = _toDoService.Add(item);
            return StatusCode(201, item);
            //_toDoService.Add(item);
        }

        [HttpPut("{id:int}")]  
        public ActionResult Update([FromRoute(Name = "id")] int id, [FromBody] ToDoItem item)
        {
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var existingItem = _toDoService.GetById(id);
            if (existingItem == null || existingItem.UserId != userId.Value)
            {
                return NotFound();
            }

            item.Id = id;
            item.UserId = userId.Value;
            _toDoService.Update(id, item);

            return Ok(item);

            //try
            //{
            //    _toDoService.Update(id, item);
            //    return Ok(item);
            //}
            //catch (Exception)
            //{
            //    return NotFound();
            //}
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)  
        {
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var existingItem = _toDoService.GetById(id);
            if (existingItem == null || existingItem.UserId != userId.Value)
            {
                return NotFound();
            }

            _toDoService.Delete(id);

            return NoContent();

            //try
            //{
            //    _toDoService.Delete(id);
            //    return NoContent();
            //}
            //catch (Exception)
            //{
            //    return NotFound();         
            //}
        }
    }
}
