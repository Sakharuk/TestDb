using System.Collections.Generic;
using Test.DataAccess.Base;
using Test.DataAccess.Interfaces;
using Test.Services.Models;
using System.Linq;
using System;

namespace Test.DataAccess.Repositories
{
    public class PersonRepository : EntityRepository<Person>, IPersonRepository
    {
        public PersonRepository(ITestDBContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<PersonResult> Get(int startRow, int count, Func<PersonResult, Object> orderByFunc, bool reverse)
        {
            var query = (from person in this.dbContext.Set<Person>()
                          join mark in this.dbContext.Set<Mark>() on person.PeopleID equals mark.PeopleID into j
                          from t in j.DefaultIfEmpty()
                          select new PersonResult
                          {
                              Id = person.PeopleID,
                              FirstName = person.FirstName,
                              LastName = person.LastName,
                              Value = t != null ? t.Value : null
                          });

            var result = reverse ? query.OrderByDescending(orderByFunc) : query.OrderBy(orderByFunc);

            return result.Skip(startRow - 1).Take(count).ToList();
        }

        public int GetCount()
        {
            var result = (from person in this.dbContext.Set<Person>()                            
                         select person).Count();
            return result;
        }

        public bool GetIsFirstNameUnique(string firstName, int? id)
        {
            var result = this.dbContext.Set<Person>().Any(e => e.FirstName.Equals(firstName) && e.PeopleID != id);
            return !result;
        }

        public bool GetIsLastNameUnique(string lastName, int? id)
        {
            var result = this.dbContext.Set<Person>().Any(e => e.LastName.Equals(lastName) && e.PeopleID != id);
            return !result;
        }
    }
}
