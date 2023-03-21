using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding a new person
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "{0} cannot be blank")]
        [EmailAddress(ErrorMessage = "{0} value should be a valid email address")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsletters { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                Name = Name,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryId = CountryId,
                Address = Address,
                ReceiveNewsletters = ReceiveNewsletters,
            };
        }
    }
}
