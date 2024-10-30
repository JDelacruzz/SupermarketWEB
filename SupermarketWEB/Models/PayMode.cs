using System.ComponentModel.DataAnnotations;

namespace SupermarketWEB.Models
{
    public class PayModeModels
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty; 
    }
}