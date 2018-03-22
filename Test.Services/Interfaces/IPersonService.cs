using System.Collections.Generic;
using Test.Services.Models;

namespace Test.Services
{
    public interface IPersonService
    {
        IEnumerable<PersonResult> GetPeople(int currentPage, int itemsPerPage, string predicate, bool reverse);
        int GetPeopleCount();
        void CreatePerson(string firstName, string lastName, int? value);
        bool IsFirstNameUnique(string firstName, int? id);
        bool IsLastNameUnique(string firstName, int? id);
    }
}
