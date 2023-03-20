using Entities;
using System.Reflection;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as the return type for most of CountryService methods
    /// </summary>
    public class CountryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        // Checks if all the key values of the provided object are equal to those of the current object
        // Overriding Equals method so that Assert.Contains() can properly check if a non-primitive object exists in a collection
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(CountryResponse)) return false;

            CountryResponse countryToCompare = (CountryResponse)obj;

            return Id == countryToCompare.Id && Name == countryToCompare.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                Id = country.Id,
                Name = country.Name,
            };
        }
    }
}
