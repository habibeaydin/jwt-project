using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [Route("[controller]")] 
    [ApiController]
    public class UserController : Controller
    {
       private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var items = _userService.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public ActionResult<User> GetById([FromRoute(Name = "id")] int id)
        {
            var item = _userService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult Add([FromBody] User user)
        {
            _userService.Add(user);
            return StatusCode(201, user);
        }

        [HttpPut("{id:int}")]
        public ActionResult Update([FromRoute(Name = "id")] int id, [FromBody] User user)
        {
            try
            {
                _userService.Update(id, user);
                return Ok(user);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
