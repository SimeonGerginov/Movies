using System.Collections.Generic;
using Movies.Core.Models;

namespace Movies.Services.Contracts
{
    public interface IPersonService
    {
        void AddPerson(Person person);

        bool DeletePerson(int personId);

        void UpdatePerson(Person personToUpdate);

        byte[] GetPersonImage(int personId);

        IEnumerable<Person> GetAllPeople();
    }
}
