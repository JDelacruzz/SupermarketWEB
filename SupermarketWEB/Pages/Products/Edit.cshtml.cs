using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWEB.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly SupermarketContext _context;

        public EditModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            Categories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToUpdate = await _context.Products.FindAsync(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Product>(
                productToUpdate,
                "Product", // Prefix for form value.
                p => p.Name, p => p.Price, p => p.Stock, p => p.CategoryId))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            Categories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            return Page();
        }
    }
}
