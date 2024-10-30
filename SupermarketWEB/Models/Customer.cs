using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SupermarketWEB.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Document_Number { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }

        public string Birth_Day { get; set; }

        public string Phone_Number { get; set; }

        public string Email { get; set; }
    }
}
