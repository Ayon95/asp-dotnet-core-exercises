using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as the return type for most of PersonService methods
    /// </summary>
    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsletters { get; set; }
        public double? Age { get; set; }

        /// <summary>
        /// Compares the current PersonResponse object with the provided PersonResponse object
        /// </summary>
        /// <param name="obj">PersonResponse object used in comparison</param>
        /// <returns>true or false indicating whether or not all the key values of both objects match</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;
            PersonResponse personToCompare = (PersonResponse)obj;
            return Id == personToCompare.Id && Name == personToCompare.Name && DateOfBirth == personToCompare.DateOfBirth && Gender == personToCompare.Gender && CountryId == personToCompare.CountryId && Country == personToCompare.Country && Address == personToCompare.Address && ReceiveNewsletters == personToCompare.ReceiveNewsletters;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                CountryId = person.CountryId,
                Address = person.Address,
                ReceiveNewsletters = person.ReceiveNewsletters,
                Age = person.DateOfBirth != null ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null,
            };
        }
    }
}
