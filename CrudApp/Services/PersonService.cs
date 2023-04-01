using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System.Reflection;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _persons;
        private readonly ICountryService _countryService;
        public PersonService()
        {
            _persons = new List<Person>();
            _countryService = new CountryService();
        }
        private PersonResponse ConvertToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetCountryById(personResponse.CountryId)?.Name;
            return personResponse;
        }
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }
            // Model validation
            ValidationHelper.ValidateModel(personAddRequest);

            Person person = personAddRequest.ToPerson();
            person.Id = Guid.NewGuid();

            _persons.Add(person);

            return ConvertToPersonResponse(person);

        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(person => ConvertToPersonResponse(person)).ToList();
        }

        public PersonResponse? GetPersonById(Guid? id)
        {
            if (id == null) return null;

            Person? person = _persons.FirstOrDefault(person => person.Id == id);

            if (person == null) return null;

            return ConvertToPersonResponse(person);
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchTerm)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            if (string.IsNullOrEmpty(searchTerm) || string.IsNullOrEmpty(searchBy)) return allPersons;

            PropertyInfo? searchProperty = typeof(PersonResponse).GetProperty(searchBy);

            if (searchProperty == null) return new List<PersonResponse>();

            List<PersonResponse> filteredPersons = allPersons.FindAll(person =>
            {
                string? value = searchProperty.GetValue(person)?.ToString();
                if (value == null) return false;
                return value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
            });
            return filteredPersons;
        }
    }
}
