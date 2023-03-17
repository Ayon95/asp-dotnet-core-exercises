using System.ComponentModel.DataAnnotations;
using ECommerceAppModelBindingValidation.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerceAppModelBindingValidation.Models
{
    public class Order
    {
        [BindNever]
        public int? OrderNumber { get; set; }

        [Display(Name = "Order Date")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [MinimumDateValidator("2001-01-01")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "Invoice Price")]
        [Required(ErrorMessage = "{0} must be provided")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} must be a positive number")]
        [TotalPriceValidator("Products", ErrorMessage = "{0} does not match with the total cost of the specified products in the order.")]
        public double? InvoicePrice { get; set; }

        [Required(ErrorMessage = "{0} must be provided")]
        [ProductsListMinimumSize(1)]
        public List<Product>? Products { get; set; }
    }
}
