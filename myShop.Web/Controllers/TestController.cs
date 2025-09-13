using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace myShop.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IEmailSender _emailSender;

        public TestController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Send()
        {
            try
            {
                await _emailSender.SendEmailAsync(
                    "eslam25elsayed12@gmail.com",
                    "Test Email",
                    "<h2>Hello from MyShop!</h2><p>This is a test email.</p>"
                );

                return Content("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

    }
}
