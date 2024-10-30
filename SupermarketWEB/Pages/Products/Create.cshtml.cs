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
        public class CreateModel : PageModel
        {
            private readonly SupermarketContext _context;

            public CreateModel(SupermarketContext context)
            {
                _context = context;
            }

            [BindProperty]
            public Product Product { get; set; } = default!;

            public IEnumerable<SelectListItem> Categories { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    Categories = await _context.Categories
                        .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Name
                        }).ToListAsync();

                    return Page();
                }

                _context.Products.Add(Product);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
        }
    }
