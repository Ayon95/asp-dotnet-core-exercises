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
        // When a valid country name is provided, a country object should be added to the list of countries
        [Fact]
        public void AddCountry_ValidCountryData()
        {
            // Arrange
            CountryAddRequest? requestData = new CountryAddRequest() { Name = "Bangladesh" };
            // Act
            CountryResponse response = _countryService.AddCountry(requestData);
            // Assert
            Assert.True(response.Id != Guid.Empty);
        }

    }
}