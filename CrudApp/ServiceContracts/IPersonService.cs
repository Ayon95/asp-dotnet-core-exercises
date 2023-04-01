using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IPersonService
    {
        PersonResponse AddPerson(PersonAddRequest? person);
        List<PersonResponse> GetAllPersons();
        PersonResponse? GetPersonById(Guid? id);
        /// <summary>
        /// Returns a list of PersonResponse objects that match the provided search criteria
        /// </summary>
        /// <param name="searchBy">the property of Person class that is used for searching</param>
        /// <param name="searchTerm">the value that is used for searching</param>
        /// <returns></returns>
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchTerm);
    }
}
