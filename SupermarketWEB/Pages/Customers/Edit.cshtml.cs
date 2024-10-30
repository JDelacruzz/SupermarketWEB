using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Threading.Tasks;

namespace SupermarketWEB.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly SupermarketContext _context;

        public EditModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers.FindAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customerToUpdate = await _context.Customers.FindAsync(Customer.Id);

            if (customerToUpdate == null)
            {
                return NotFound();
            }

            customerToUpdate.Document_Number = Customer.Document_Number;
            customerToUpdate.First_Name = Customer.First_Name;
            customerToUpdate.Last_Name = Customer.Last_Name;
            customerToUpdate.Address = Customer.Address;
            customerToUpdate.Birth_Day = Customer.Birth_Day;
            customerToUpdate.Phone_Number = Customer.Phone_Number;
            customerToUpdate.Email = Customer.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
