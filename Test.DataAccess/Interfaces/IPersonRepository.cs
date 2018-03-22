using System;
using System.Collections.Generic;
using Test.DataAccess.BaseInterfaces;
using Test.Services.Models;

namespace Test.DataAccess.Interfaces
{
    public interface IPersonRepository : IEntityRepository<Person>
    {
        IEnumerable<PersonResult> Get(int startRow, int endRow, Func<PersonResult, Object> orderByFunc, bool reverse);
        int GetCount();
        bool GetIsFirstNameUnique(string firstName, int? id);
        bool GetIsLastNameUnique(string lastName, int? id);

    }
}
