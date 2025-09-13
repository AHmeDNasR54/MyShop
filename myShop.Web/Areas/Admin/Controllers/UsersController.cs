using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myShop.DataAccess.Data;
using myShop.Utilities;
using System.Security.Claims;

namespace myShop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize( Roles = SD.AdminRole)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            return View(_context.ApplicationUsers.Where(u=>u.Id!=userid).ToList());
        }
        public IActionResult LockUnlock(string? id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.LockoutEnd == null || user.LockoutEnd < DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1);
            }
            else
            {
                user.LockoutEnd = DateTime.Now;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.ApplicationUsers.Remove(user);
            _context.SaveChanges();
            TempData["success"] = "User deleted successfully!";

            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
    }
}
