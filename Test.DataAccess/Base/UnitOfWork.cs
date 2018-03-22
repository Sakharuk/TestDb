using Test.DataAccess.BaseInterfaces;
using Test.DataAccess.Interfaces;

namespace Test.DataAccess.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;

        public UnitOfWork(ITestDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDbContext Context
        {
            get
            {
                return _dbContext;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
