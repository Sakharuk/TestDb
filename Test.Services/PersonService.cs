using System;
using System.Collections.Generic;
using System.Linq;
using Test.DataAccess;
using Test.DataAccess.BaseInterfaces;
using Test.DataAccess.Interfaces;
using Test.Services.Models;

namespace Test.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMarkRepository _markRepository;
        private readonly IUnitOfWork _uow;

        public PersonService(IPersonRepository personRepository,
            IMarkRepository markRepository,
            IUnitOfWork uow)
        {
            _personRepository = personRepository;
            _markRepository = markRepository;
            _uow = uow;
        }

        public IEnumerable<PersonResult> GetPeople(int currentPage, int itemsPerPage, string predicate, bool reverse)
        {
            Func<PersonResult, Object> orderByFunc = null;

            switch (predicate)
            {
                case "Id":
                    orderByFunc = item => item.Id;
                    break;
                case "FirstName":
                    orderByFunc = item => item.FirstName;
                    break;
                case "LastName":
                    orderByFunc = item => item.LastName;
                    break;
                case "Value":
                    orderByFunc = item => item.Value;
                    break;
                default:
                    orderByFunc = item => item.Id;
                    break;
            }

            int startRow = (currentPage - 1) * itemsPerPage + 1;
            int count = itemsPerPage;

            var result = _personRepository.Get(startRow, count, orderByFunc, reverse).ToList();

            return result;
        }

        public int GetPeopleCount()
        {            
            var result = _personRepository.GetCount();

            return result;
        }

        public void CreatePerson(string firstName, string lastName, int? value)
        {
            var newPerson = new Person
            {
                FirstName = firstName,
                LastName = lastName
            };       

            _personRepository.Create(newPerson);

            var newMark = new Mark
            {
                Value = value,
                Person = newPerson
            };
            _markRepository.Create(newMark);
            _uow.Save();
        }

        public bool IsFirstNameUnique(string firstName, int? id)
        {
            return _personRepository.GetIsFirstNameUnique(firstName, id);
        }

        public bool IsLastNameUnique(string lastName, int? id)
        {
            return _personRepository.GetIsLastNameUnique(lastName, id);
        }
    }
}
