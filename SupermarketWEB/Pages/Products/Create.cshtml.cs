using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public class InputModel
        {
            [Required(ErrorMessage = "El nombre es obligatorio.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "El precio es obligatorio.")]
            [Range(0, int.MaxValue, ErrorMessage = "El precio debe ser un número positivo.")]
            public int Price { get; set; } 

            [Required(ErrorMessage = "El stock es obligatorio.")]
            [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número positivo.")]
            public int Stock { get; set; } 

            [Required(ErrorMessage = "La categoría es obligatoria")]
            [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
            public int CategoryId { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        // Lista de categorías para el dropdown
        public IEnumerable<SelectListItem> Categories { get; set; } = default!;

        // Método para cargar las categorías
        private async Task LoadCategories()
        {
            Categories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            if (!Categories.Any())
            {
                ModelState.AddModelError("", "No hay categorías disponibles. Debe crear al menos una categoría primero.");
            }
        }

        // Acción GET para cargar la página
        public async Task<IActionResult> OnGetAsync()
        {
            await LoadCategories();
            return Page();
        }

        // Acción POST para procesar la creación del producto
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return Page();
            }

            // Crear una instancia de Product con los datos de Input
            var product = new Product
            {
                Name = Input.Name,
                Price = Input.Price,
                Stock = Input.Stock,
                CategoryId = Input.CategoryId
            };

            // Guardar el producto en la base de datos
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Producto creado exitosamente.";
            return RedirectToPage("./Index");
        }
    }
}
