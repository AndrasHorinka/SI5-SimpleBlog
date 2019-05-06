using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySimpleBlog.Data;

namespace MySimpleBlog.Controllers
{
    public class QueriesController : Controller
    {
        private readonly APIContext _context;

        public QueriesController(APIContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var usr = (from x in await _context.Users.Include(u => u.Posts).ToListAsync() select x)
                    .OrderByDescending(u => u.GetNumberOfPosts())
                    .FirstOrDefault();
            //return View((from x in await _context.Users.ToListAsync() select x.Posts.Count).Max());
            return View(usr);
        }
    }
}
