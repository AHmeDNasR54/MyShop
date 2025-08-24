using Microsoft.AspNetCore.Mvc;
using myShop.DataAccess.Data;
using myShop.Entities.Models;
using myShop.Entities.Repositories;


namespace myShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        public IunitOfWork _unitOfWork { get; }

        //private readonly ApplicationDbContext _context;


        public CategoryController(IunitOfWork unitOfWork)//ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            //_context = context;
        }


        public IActionResult Index()
        {
            // Fetch categories from the database
            var categories = _unitOfWork.Categories.GetAll(); //_context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Return the view for creating a new category
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // Add the new category to the database
                //_context.Categories.Add(category);
                //_context.SaveChanges();
                _unitOfWork.Categories.Add(category);
                _unitOfWork.complete();
                TempData["Create"] = "Data has Ctreated successfully";

                return RedirectToAction("Index");
            }
            // If model state is invalid, return the same view with validation errors
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Fetch the category by id
            var category = _unitOfWork.Categories.GetFirstorDefault(c => c.Id == id);// _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                // Update the category in the database
                //_context.Categories.Update(category);
                //_context.SaveChanges();
                _unitOfWork.Categories.Update(category);
                _unitOfWork.complete();

                TempData["Update"] = "Data has Updated successfully";

                return RedirectToAction("Index");
            }
            // If model state is invalid, return the same view with validation errors
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Fetch the category by id
            var category = _unitOfWork.Categories.GetFirstorDefault(c => c.Id == id);// _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            // Fetch the category by id
            var category = _unitOfWork.Categories.GetFirstorDefault(c => c.Id == id);// _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            // Remove the category from the database
            //_context.Categories.Remove(category);
            //_context.SaveChanges();
            _unitOfWork.Categories.Remove(category);
            _unitOfWork.complete();
            TempData["Delete"] = "Data has deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
