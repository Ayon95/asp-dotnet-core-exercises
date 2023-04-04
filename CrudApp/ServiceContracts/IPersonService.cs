using ServiceContracts.DTO;
using ServiceContracts.Enums;

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
        /// <summary>
        /// Sorts a list of PersonResponse objects based on the provided sorting criteria
        /// </summary>
        /// <param name="persons">The list of persons to sort</param>
        /// <param name="sortBy">The property based on which persons should be sorted</param>
        /// <param name="sortOrder">The sort order; by default it is ASC</param>
        /// <returns>Returns a sorted list of PersonResponse objects</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> persons, string sortBy, SortOrderOptions sortOrder = SortOrderOptions.ASC);
    }
}
