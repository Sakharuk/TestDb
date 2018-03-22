using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess.BaseInterfaces;

namespace Test.DataAccess.Interfaces
{
    public interface ITestDBContext : IDbContext, IDisposable
    {
        IDbSet<Person> People { get; }
        IDbSet<Mark> Marks { get; }
    }
}
