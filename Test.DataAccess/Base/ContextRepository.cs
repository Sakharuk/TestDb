using Test.DataAccess.BaseInterfaces;

namespace Test.DataAccess.Base
{
    public class ContextRepository
    {
        private IDbContext _dbContext;
        protected IDbContext dbContext
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }

        public ContextRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
