using System.ComponentModel.DataAnnotations;

namespace ECommerceAppModelBindingValidation.CustomValidators
{
    public class MinimumDateValidator : ValidationAttribute
    {
        public DateTime MinimumDate { get; set; }
        public string DefaultErrorMessage = "{0} must be greater than or equal to {1}";

        public MinimumDateValidator(string minimumDateString)
        {
            MinimumDate = Convert.ToDateTime(minimumDateString);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;

                if (date < MinimumDate)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, validationContext.MemberName, MinimumDate.ToString("yyyy-MM-dd")));
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
