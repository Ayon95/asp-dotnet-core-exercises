using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Validates an object and throws ArgumentException if there is any error
        /// </summary>
        /// <param name="obj">The model object that is to be validated</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateModel(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
