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
            List<PersonResponse> persons = _personService.GetAllPersons();

            Assert.True(addedPerson.Id != Guid.Empty);
            Assert.Contains(addedPerson, persons);
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

        #region GetAllPersons
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            List<PersonResponse> persons = _personService.GetAllPersons();
            Assert.Empty(persons);
        }

        [Fact]
        public void GetAllPersons_AddSomePersons()
        {
            PersonResponse personResponse1 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            PersonResponse personResponse2 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Mary Smith",
                Email = "mary.smith@example.com",
                DateOfBirth = DateTime.Parse("1995-03-10"),
                Gender = GenderOptions.Female,
                CountryId = Guid.NewGuid(),
                Address = "123 Marvelous Drive",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> persons = _personService.GetAllPersons();

            Assert.Contains(personResponse1, persons);
            Assert.Contains(personResponse2, persons);
        }
        #endregion

        #region GetFilteredPersons
        // Should return an empty list if persons list is empty
        [Fact]
        public void GetFilteredPersons_EmptyList()
        {
            List<PersonResponse> result = _personService.GetFilteredPersons("Name", "John Doe");
            Assert.Empty(result);
        }

        // Should return an empty list if search field does not exist
        [Fact]
        public void GetFilteredPersons_InvalidSearchField()
        {
            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> result = _personService.GetFilteredPersons("Location", "123 Amazing Street");

            Assert.Empty(result);
        }

        // Should return an empty list if search term does not match
        [Fact]
        public void GetFilteredPersons_SearchTermDoesNotMatch()
        {
            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> result = _personService.GetFilteredPersons("Address", "456 Amazing Street");

            Assert.Empty(result);
        }

        // Should return a list of all persons if search term is empty or null
        [Fact]
        public void GetFilteredPersons_EmptySearchTerm()
        {
            PersonResponse person1 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            PersonResponse person2 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Michael Smith",
                Email = "michael.smith@example.com",
                DateOfBirth = DateTime.Parse("1994-03-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> result = _personService.GetFilteredPersons("Name", "");

            Assert.Contains(person1, result);
            Assert.Contains(person2, result);
        }

        // Should return a list of all persons if search field is empty or null
        [Fact]
        public void GetFilteredPersons_EmptySearchField()
        {
            PersonResponse person1 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            PersonResponse person2 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Michael Smith",
                Email = "michael.smith@example.com",
                DateOfBirth = DateTime.Parse("1994-03-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> result = _personService.GetFilteredPersons("", "Michael Smith");

            Assert.Contains(person1, result);
            Assert.Contains(person2, result);
        }

        // Should return a list of persons that match the search criteria
        [Fact]
        public void GetFilteredPersons_ValidSearchFieldAndSearchTerm()
        {
            PersonResponse person1 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            PersonResponse person2 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                DateOfBirth = DateTime.Parse("1994-03-15"),
                Gender = GenderOptions.Female,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            PersonResponse person3 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Emily Jones",
                Email = "emily.jones@example.com",
                DateOfBirth = DateTime.Parse("1996-08-21"),
                Gender = GenderOptions.Female,
                CountryId = Guid.NewGuid(),
                Address = "456 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> result = _personService.GetFilteredPersons("Address", "123 Amazing Street");

            Assert.Contains(person1, result);
            Assert.Contains(person2, result);
            Assert.DoesNotContain(person3, result);
        }

        // Searching should be partial and case insensitive
        [Fact]
        public void GetFilteredPersons_PartialCaseInsensitiveMatch()
        {
            PersonResponse person1 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            PersonResponse person2 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Michael Smith",
                Email = "michael.smith@example.com",
                DateOfBirth = DateTime.Parse("1994-03-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            PersonResponse person3 = _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Emily Jones",
                Email = "emily.jones@example.com",
                DateOfBirth = DateTime.Parse("1996-08-21"),
                Gender = GenderOptions.Female,
                CountryId = Guid.NewGuid(),
                Address = "456 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> result = _personService.GetFilteredPersons("Name", "emi");

            Assert.Contains(person3, result);
            Assert.DoesNotContain(person1, result);
            Assert.DoesNotContain(person2, result);
        }
        #endregion

        #region GetSortedPersons
        // Should return an empty list if persons list is empty
        [Fact]
        public void GetSortedPersons_EmptyList()
        {
            List<PersonResponse> persons = _personService.GetAllPersons();
            List<PersonResponse> result = _personService.GetSortedPersons(persons, "Name", SortOrderOptions.ASC);
            Assert.Empty(result);
        }
        // Should return the provided list if the sortBy is null or empty
        [Fact]
        public void GetSortedPersons_EmptyOrNullSortBy()
        {
            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Michael Smith",
                Email = "michael.smith@example.com",
                DateOfBirth = DateTime.Parse("1994-03-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> persons = _personService.GetAllPersons();

            List<PersonResponse> sortedPersons = _personService.GetSortedPersons(persons, "");

            for (int i = 0; i < sortedPersons.Count; i++)
            {
                Assert.Equal(persons[i], sortedPersons[i]);
            }
        }
        // Should return the provided list if the sortBy field does not exist
        [Fact]
        public void GetSortedPersons_NonexistentSortByField()
        {
            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Michael Smith",
                Email = "michael.smith@example.com",
                DateOfBirth = DateTime.Parse("1994-03-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> persons = _personService.GetAllPersons();

            List<PersonResponse> sortedPersons = _personService.GetSortedPersons(persons, "First Name");

            for (int i = 0; i < sortedPersons.Count; i++)
            {
                Assert.Equal(persons[i], sortedPersons[i]);
            }
        }

        // Should return a properly sorted list in case of valid sortBy and default ASC sortOrder
        [Fact]
        public void GetSortedPersons_ValidAscendingSort()
        {
            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Michael Smith",
                Email = "michael.smith@example.com",
                DateOfBirth = DateTime.Parse("1994-03-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> persons = _personService.GetAllPersons();

            List<PersonResponse> sortedPersons = _personService.GetSortedPersons(persons, "Name");
            List<PersonResponse> expectedSortedPersons = persons.OrderBy(person => person.Name).ToList();

            for (int i = 0; i < sortedPersons.Count; i++)
            {
                Assert.Equal(expectedSortedPersons[i], sortedPersons[i]);
            }
        }

        // Should return a properly sorted list in case of valid sortBy and DESC sortOrder
        [Fact]
        public void GetSortedPersons_ValidDescendingSort()
        {
            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Parse("1992-01-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Amazing Street",
                ReceiveNewsletters = true,
            });

            _personService.AddPerson(new PersonAddRequest()
            {
                Name = "Michael Smith",
                Email = "michael.smith@example.com",
                DateOfBirth = DateTime.Parse("1994-03-15"),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Splendid Crescent",
                ReceiveNewsletters = true,
            });

            List<PersonResponse> persons = _personService.GetAllPersons();

            List<PersonResponse> sortedPersons = _personService.GetSortedPersons(persons, "Name", SortOrderOptions.DESC);
            List<PersonResponse> expectedSortedPersons = persons.OrderByDescending(person => person.Name).ToList();

            for (int i = 0; i < sortedPersons.Count; i++)
            {
                Assert.Equal(expectedSortedPersons[i], sortedPersons[i]);
            }
        }
        #endregion
    }
}
