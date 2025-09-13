using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using myShop.Utilities.Service;
using System.ComponentModel.DataAnnotations;
namespace myShop.Web.Areas.Identity.Pages.Account;

public class VerifyCodeModel : PageModel
{
    private readonly VerificationService _verificationService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public VerifyCodeModel(
        VerificationService verificationService,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _verificationService = verificationService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    [BindProperty]
    public string UserId { get; set; }

    [BindProperty]
    public string ReturnUrl { get; set; }
     
    public class InputModel
    {
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Code must be 6 digits.")]
        public string Code { get; set; }
    }

    public void OnGet(string userId, string returnUrl = null)
    {
        UserId = userId;
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var success = _verificationService.VerifyCode(UserId, Input.Code);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, "Invalid or expired code.");
            return Page();
        }

        var user = await _userManager.FindByIdAsync(UserId);
        if (user != null)
        {
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            // Sign in user
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        return LocalRedirect(ReturnUrl ?? "~/");
    }
}
