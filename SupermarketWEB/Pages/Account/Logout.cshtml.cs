using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Autenticacion.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            // Inicia el proceso de cierre de sesión para el esquema de autenticación "MyCookieAuth"
            await HttpContext.SignOutAsync("MyCookieAuth");

            // Redirige al usuario a la página de inicio ("/Index") después de cerrar sesión
            return RedirectToPage("/Index");
        }
    }
}
