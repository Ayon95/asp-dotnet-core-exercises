using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly List<Country> _countries;

        public CountryService()
        {
            _countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            if (countryAddRequest.Name == null)
            {
                throw new ArgumentException($"{nameof(countryAddRequest.Name)} is null");
            }

            if (_countries.Any(country => country.Name == countryAddRequest.Name))
            {
                throw new ArgumentException("A country with the provided name already exists");
            }

            Country country = countryAddRequest.ToCountry();

            country.Id = Guid.NewGuid();

            _countries.Add(country);

            return country.ToCountryResponse();
        }
    }
}