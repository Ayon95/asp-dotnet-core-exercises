using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace Tests
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personService;

        public PersonServiceTest()
        {
            _personService = new PersonService();
        }

        #region AddPerson
        // When PersonAddRequest is null, AddPerson() method should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(null);
            });
        }

        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            PersonAddRequest? requestData = new PersonAddRequest() { Name = null };
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(requestData);
            });
        }

        [Fact]
        public void AddPerson_ValidPersonData()
        {
            PersonAddRequest? personData = new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            };

            PersonResponse addedPerson = _personService.AddPerson(personData);

            Assert.True(addedPerson.Id != Guid.Empty);
        }
        #endregion

        #region GetPersonById
        [Fact]
        public void GetPersonById_NullId()
        {
            PersonResponse? response = _personService.GetPersonById(null);
            Assert.Null(response);
        }

        [Fact]
        public void GetPersonById_NonexistentId()
        {
            Guid id = Guid.NewGuid();
            PersonResponse? response = _personService.GetPersonById(id);
            Assert.Null(response);
        }

        [Fact]
        public void GetPersonById_ValidId()
        {
            PersonAddRequest requestData = new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            };
            PersonResponse addedPerson = _personService.AddPerson(requestData);

            PersonResponse? response = _personService.GetPersonById(addedPerson.Id);

            Assert.Equal(addedPerson, response);
        }
        #endregion
    }


}
