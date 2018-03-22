using System;

namespace Test.DataAccess.BaseInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContext Context { get; }

        void Save();
    }
}