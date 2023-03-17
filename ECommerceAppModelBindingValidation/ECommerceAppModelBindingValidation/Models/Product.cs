using System.ComponentModel.DataAnnotations;

namespace ECommerceAppModelBindingValidation.Models
{
    public class Product
    {
        [Display(Name = "Product Code")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be a positive number")]
        public int ProductCode { get; set; }

        [Required(ErrorMessage = "{0} cannot be blank")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} must be a positive number")]
        public double Price { get; set; }

        [Required(ErrorMessage = "{0} cannot be blank")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be a valid positive number")]
        public int Quantity { get; set; }
    }
}
