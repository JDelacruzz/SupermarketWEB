using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Autenticacion.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Autenticacion.Pages.Account
{
    public class LoginModel : PageModel
    {
        private const int MaxFailedAttempts = 3; // Número máximo de intentos
        private const int LockoutMinutes = 5; // Tiempo de bloqueo en minutos

        [BindProperty]
        public User User { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            ErrorMessage = HttpContext.Session.GetString("ErrorMessage");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int failedAttempts = HttpContext.Session.GetInt32("FailedAttempts") ?? 0;

            string lockoutEndString = HttpContext.Session.GetString("LockoutEnd");
            DateTime? lockoutEnd = null;
            if (DateTime.TryParse(lockoutEndString, out DateTime parsedDate))
            {
                lockoutEnd = parsedDate;
            }

            if (lockoutEnd.HasValue && lockoutEnd > DateTime.Now)
            {
                ErrorMessage = $"Demasiados intentos fallidos. Intente nuevamente a las {lockoutEnd.Value.ToLongTimeString()}";
                return Page();
            }

            if (User.Email == "correo@gmail.com" && User.Password == "12345")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email,User.Email),
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                HttpContext.Session.Remove("FailedAttempts");
                HttpContext.Session.Remove("LockoutEnd");
                HttpContext.Session.Remove("ErrorMessage");

                return RedirectToPage("/index");
            }
            else
            {
                failedAttempts++;
                HttpContext.Session.SetInt32("FailedAttempts", failedAttempts);

                if (failedAttempts >= MaxFailedAttempts)
                {
                    lockoutEnd = DateTime.Now.AddMinutes(LockoutMinutes);
                    HttpContext.Session.SetString("LockoutEnd", lockoutEnd.Value.ToString());
                    ErrorMessage = $"Demasiados intentos fallidos. Intente nuevamente a las {lockoutEnd.Value.ToLongTimeString()}";
                }
                else
                {
                    ErrorMessage = "Correo o contraseña incorrectos.";
                }
                HttpContext.Session.SetString("ErrorMessage", ErrorMessage);
            }

            return Page();
        }
    }
}
