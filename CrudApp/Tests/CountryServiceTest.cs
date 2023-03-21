using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace Tests
{
    public class CountryServiceTest
    {
        private readonly ICountryService _countryService;

        public CountryServiceTest()
        {
            _countryService = new CountryService();
        }

        #region AddCountry
        // When CountryAddRequest is null, AddCountry() method should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange
            CountryAddRequest? requestData = null;
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _countryService.AddCountry(requestData);
            });
        }

        // When Name is null, AddCountry() should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arrange
            CountryAddRequest? requestData = new CountryAddRequest() { Name = null };
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countryService.AddCountry(requestData);
            });
        }
        // When Name is duplicate, AddCountry() should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest? requestData1 = new CountryAddRequest() { Name = "Canada" };
            CountryAddRequest? requestData2 = new CountryAddRequest() { Name = "Canada" };
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countryService.AddCountry(requestData1);
                _countryService.AddCountry(requestData2);
            });
        }
        // When a valid country name is provided, a country object should be added to the list of countries and that CountryResponse object should have a valid ID
        [Fact]
        public void AddCountry_ValidCountryData()
        {
            // Arrange
            CountryAddRequest? requestData = new CountryAddRequest() { Name = "Bangladesh" };
            // Act
            CountryResponse response = _countryService.AddCountry(requestData);
            List<CountryResponse> countries = _countryService.GetAllCountries();
            // Assert
            Assert.True(response.Id != Guid.Empty);
            Assert.Contains(response, countries);
        }
        #endregion

        #region GetAllCountries
        // By default, the list of countries should be empty
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            // Act
            List<CountryResponse> countries = _countryService.GetAllCountries();
            // Assert
            Assert.Empty(countries);
        }

        // If the list is not empty, the method should return a list of all the added countries
        [Fact]
        public void GetAllCountries_AddSomeCountries()
        {
            // Arrange
            List<CountryAddRequest> countriesToBeAdded = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { Name = "USA" },
                new CountryAddRequest() { Name = "Canada" }
            };

            List<CountryResponse> addedCountries = new List<CountryResponse>();

            // Act
            foreach (CountryAddRequest country in countriesToBeAdded)
            {
                CountryResponse countryResponse = _countryService.AddCountry(country);
                addedCountries.Add(countryResponse);
            }

            List<CountryResponse> actualCountries = _countryService.GetAllCountries();

            // Assert
            foreach (CountryResponse addedCountry in addedCountries)
            {
                Assert.Contains(addedCountry, actualCountries);
            }

        }
        #endregion

        #region GetCountryById
        // if the provided country ID is null, it should return null
        [Fact]
        public void GetCountryById_NullId()
        {
            // Arrange
            Guid? id = null;
            // Act
            CountryResponse? response = _countryService.GetCountryById(id);
            // Assert
            Assert.Null(response);

        }
        // if a country with the provided ID does not exist, it should return null
        [Fact]
        public void GetCountryById_NonexistentId()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            // Act
            CountryResponse? response = _countryService.GetCountryById(id);
            // Assert
            Assert.Null(response);
        }
        // if a valid country ID is provided, it should return the matching country
        [Fact]
        public void GetCountryById_ValidId()
        {
            // Arrange
            CountryAddRequest countryToBeAdded = new CountryAddRequest() { Name = "Canada" };
            CountryResponse addedCountry = _countryService.AddCountry(countryToBeAdded);
            // Act
            CountryResponse? response = _countryService.GetCountryById(addedCountry.Id)!;
            // Assert
            Assert.Equal(addedCountry, response);
        }
        #endregion
    }
}