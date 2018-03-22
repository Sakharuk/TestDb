using Test.DataAccess.Base;
using Test.DataAccess.Interfaces;

namespace Test.DataAccess.Repositories
{
    public class MarkRepository : EntityRepository<Mark>, IMarkRepository
    {
        public MarkRepository(ITestDBContext dbContext) : base(dbContext)
        {
        }
    }
}
