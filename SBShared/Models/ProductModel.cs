using System.ComponentModel.DataAnnotations;

namespace SBShared.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Barcode { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public double BuyingPrice { get; set; }
        public string ConfidentialData { get; set; }
    }
}
