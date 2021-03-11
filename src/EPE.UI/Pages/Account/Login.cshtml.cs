using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        SignInManager<IdentityUser> _signInManager;
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Usuario { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    Usuario.Username,
                    Usuario.Password,
                    Usuario.RememberMe,
                    false
                );
                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                
                ModelState.AddModelError(string.Empty, "Invalid log in attempt.");
            }

            return Page();
        }
    }

    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
