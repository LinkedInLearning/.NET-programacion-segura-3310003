using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Wpm.Web.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginInputModel Input { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Input.Username == "admin" && Input.Password == "admin")
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, Input.Username)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal principal = new(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            return RedirectToPage("/Index");
        }

        ModelState.AddModelError(string.Empty, "Invalid login.");
        return Page();
    }
}

public class LoginInputModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}