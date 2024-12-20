using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModels
{
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;

        public IndexModel(SupermarketContext context)
        {
            _context = context;
        }

        public IList<PayModeModels> PayModels { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PayModels != null)
            {
                PayModels = await _context.PayModels.ToListAsync();
            }
        }
    }
}