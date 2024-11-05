using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Autenticacion.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            // Inicia el proceso de cierre de sesi�n para el esquema de autenticaci�n "MyCookieAuth"
            await HttpContext.SignOutAsync("MyCookieAuth");

            // Redirige al usuario a la p�gina de inicio ("/Index") despu�s de cerrar sesi�n
            return RedirectToPage("/Index");
        }
    }
}
