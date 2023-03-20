using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to be added</param>
        /// <returns>the country object after adding it (with newly-generated country ID)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
        /// <summary>
        /// Gets all countries from the list
        /// </summary>
        /// <returns>A list of CountryResponse objects</returns>
        List<CountryResponse> GetAllCountries();
        /// <summary>
        /// Gets the country that has the provided ID
        /// </summary>
        /// <param name="Id">The ID used for searching a country</param>
        /// <returns>the matching country as a CountryResponse object</returns>
        CountryResponse? GetCountryById(Guid? id);
    }
}