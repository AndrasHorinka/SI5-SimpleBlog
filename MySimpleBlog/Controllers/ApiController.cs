using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySimpleBlog.Data;
using System.Threading.Tasks;

namespace MySimpleBlog.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly APIContext _context;

        public ApiController(APIContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/user-with-most-post")]
        public async Task<ActionResult<User>> GetUserWithMostPost()
        {
            User tempUser = new User();
            /* ez valoszinuleg nem jo igy asyncben, mert elobb megy vissza az ures user minthogy vegigiteraljon - LINQ kellene*/
            foreach (User user in await _context.Users.ToListAsync())
            {
                if (user.GetNumberOfPosts() > tempUser.GetNumberOfPosts())
                {
                    tempUser = user;
                }
            }
            return tempUser;
        }
    }
}