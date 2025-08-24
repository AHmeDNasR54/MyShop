using Microsoft.AspNetCore.Mvc;
using myShop.DataAccess.Implementation;
using myShop.Entities.Repositories;
using myShop.Utilities;
using System.Security.Claims;

namespace myShop.Web.ViewComponents
{
    public class ShoppingCartViewComponent:ViewComponent
    {
        
        public ShoppingCartViewComponent(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private readonly IunitOfWork _unitOfWork;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(SD.SessionKey) != null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessionKey));
                }
                else
                {
                    HttpContext.Session.SetInt32(SD.SessionKey, _unitOfWork.ShoppingCarts.GetAll(x => x.ApplicationUserId == claim.Value).ToList().Count());
                    return View(HttpContext.Session.GetInt32(SD.SessionKey));
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }

    }
}
