using System;
using System.Linq;

using Bytes2you.Validation;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> personRepository;

        public PersonService(IRepository<Person> personRepository)
        {
            Guard.WhenArgument(personRepository, "Person Repository").IsNull().Throw();

            this.personRepository = personRepository;
        }

        public void AddPerson(Person person)
        {
            Guard.WhenArgument(person, "Person").IsNull().Throw();

            var personExists = this.personRepository
                .GetAllFiltered(p => 
                    p.FirstName == person.FirstName && 
                    p.LastName == person.LastName && 
                    p.DateOfBirth == person.DateOfBirth)
                .Any();

            if (personExists)
            {
                throw new InvalidOperationException("Person already exists!");
            }

            person.CreatedOn = DateTime.UtcNow;
            this.personRepository.Add(person);
        }
    }
}
