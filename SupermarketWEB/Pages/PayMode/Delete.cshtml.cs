using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModels
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PayModeModels PayMode { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PayModels == null)
            {
                return NotFound();
            }

            var paymode = await _context.PayModels.FirstOrDefaultAsync(m => m.Id == id);

            if (paymode == null)
            {
                return NotFound();
            }
            else
            {
                PayMode = paymode;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PayModels == null)
            {
                return NotFound();
            }

            var paymode = await _context.PayModels.FindAsync(id);

            if (paymode != null)
            {
                PayMode = paymode;
                _context.PayModels.Remove(PayMode);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}