using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myShop.Entities.Models;
using myShop.Entities.Repositories;
using myShop.Utilities;
using System.Security.Claims;
using X.PagedList.Extensions;

namespace myShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
       
            private readonly IunitOfWork _unitofwork;

            public HomeController(IunitOfWork unitofwork)
            {
                _unitofwork = unitofwork;
            }
            public IActionResult Index(int? page)
            {
                var PageNumber = page ?? 1;
                int PageSize = 8;
      

            var products = _unitofwork.Products.GetAll().ToPagedList(PageNumber,PageSize) ;
                return View(products);
            }

        public IActionResult Details(int ProductId)
        {
            var product = _unitofwork.Products.GetFirstorDefault(v => v.Id == ProductId, Includeword: "Category");
            if(product == null)
            {
                return NotFound();
            }
            ShoppingCart obj = new ShoppingCart()
            {
                ProductId = ProductId,
                Product = product,
                Count = 1
            };
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

       
               ShoppingCart Cartobj = _unitofwork.ShoppingCarts.GetFirstorDefault(
                            u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);


            if (Cartobj == null)
            {
                _unitofwork.ShoppingCarts.Add(shoppingCart);
                _unitofwork.complete();

                HttpContext.Session.SetInt32(SD.SessionKey,
                    _unitofwork.ShoppingCarts.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count());

            }
            else
            {
                _unitofwork.ShoppingCarts.IncreaseCount(Cartobj, shoppingCart.Count);
                _unitofwork.complete();
            }
            

            return RedirectToAction("Index");
        }

    }
}
