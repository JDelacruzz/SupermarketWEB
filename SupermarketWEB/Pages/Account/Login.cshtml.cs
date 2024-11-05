using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Autenticacion.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Autenticacion.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        private const int MaxLoginAttempts = 3; 

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int failedAttempts = HttpContext.Session.GetInt32("FailedAttempts") ?? 0;

            if (failedAttempts >= MaxLoginAttempts)
            {
                ModelState.AddModelError(string.Empty, "Cuenta bloqueada temporalmente. Intenta de nuevo más tarde.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (User.Email == "correo@gmail.com" && User.Password == "12345")
            {
                HttpContext.Session.SetInt32("FailedAttempts", 0);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, User.Email),
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return RedirectToPage("/index");
            }

            failedAttempts++;
            HttpContext.Session.SetInt32("FailedAttempts", failedAttempts);

            ModelState.AddModelError(string.Empty, $"Correo o contraseña incorrecta. Intentos restantes: {MaxLoginAttempts - failedAttempts}");
            return Page();
        }
    }
}
