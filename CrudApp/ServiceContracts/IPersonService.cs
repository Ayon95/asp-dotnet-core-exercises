using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IPersonService
    {
        PersonResponse AddPerson(PersonAddRequest? person);
        List<PersonResponse> GetAllPersons();
    }
}
