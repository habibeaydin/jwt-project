// HttpContext kullanarak token içindeki kullanıcı kimliğini elde etme.

namespace ToDoListApp.Extensions
{
    public static class HttpContextExtension
    {
        public static int? GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                throw new Exception("!!!");

            var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
                return null;

            return int.Parse(userIdClaim.Value);
        }
    }

}
