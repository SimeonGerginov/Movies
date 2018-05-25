using System;
using System.Collections.Generic;
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
            person.SetPersonAge();
         
            this.personRepository.Add(person);
        }

        public bool DeletePerson(int personId)
        {
            var targetPerson = this.personRepository
                .GetAllFiltered(p => p.Id == personId)
                .FirstOrDefault();

            if (targetPerson == null)
            {
                return false;
            }

            this.personRepository.Delete(targetPerson);
            return true;
        }

        public void UpdatePerson(Person personToUpdate)
        {
            var targetPerson = this.personRepository
                .GetAllFiltered(p => p.Id == personToUpdate.Id)
                .FirstOrDefault();

            if (targetPerson != null)
            {
                targetPerson.FirstName = personToUpdate.FirstName;
                targetPerson.LastName = personToUpdate.LastName;
                targetPerson.Nationality = personToUpdate.Nationality;
                targetPerson.Gender = personToUpdate.Gender;
                targetPerson.Picture = personToUpdate.Picture;
                targetPerson.DateOfBirth = personToUpdate.DateOfBirth;
                targetPerson.ModifiedOn = DateTime.UtcNow;
                targetPerson.SetPersonAge();

                this.personRepository.Update(targetPerson);
            }
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return this.personRepository.GetAll();
        }
    }
}
