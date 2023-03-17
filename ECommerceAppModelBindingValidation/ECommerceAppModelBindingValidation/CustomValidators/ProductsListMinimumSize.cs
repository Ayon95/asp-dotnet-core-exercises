using ECommerceAppModelBindingValidation.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAppModelBindingValidation.CustomValidators
{
    public class ProductsListMinimumSize : ValidationAttribute
    {
        public int MinimumSize { get; set; }
        public string DefaultErrorMessage { get; set; } = "The list must contain at least {0} items.";
        public ProductsListMinimumSize(int minimumSize)
        {
            MinimumSize = minimumSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                List<Product> products = (List<Product>)value;

                if (products.Count < MinimumSize)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumSize));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}
