using ECommerceAppModelBindingValidation.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECommerceAppModelBindingValidation.CustomValidators
{
    // this attribute validates that the total price of a model by examining a list of products
    public class TotalPriceValidator : ValidationAttribute
    {
        public string OtherPropertyName { get; set; }
        public string DefaultErrorMessage { get; set; } = "{0} must be equal to the total price of all products.";

        public TotalPriceValidator(string otherPropertyName)
        {
            OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

            if (value != null && otherProperty != null)
            {
                // ! specifies that the value returned by GetValue will not be null
                List<Product> products = (List<Product>)otherProperty.GetValue(validationContext.ObjectInstance)!;

                double totalPrice = 0;

                foreach (Product product in products)
                {
                    totalPrice += product.Price * product.Quantity;
                }

                double actualPrice = (double)value;

                if (totalPrice != actualPrice)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, validationContext.DisplayName), new string[] { validationContext.MemberName! });
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
