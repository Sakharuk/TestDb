using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Test.DataAccess.BaseInterfaces
{
    public interface IDbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

        void MarkAsNew(object entity);

        void MarkAsModified(object entity);

        void MarkAsDeleted(object entity);

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        IEnumerable<TElement> Execute<TElement>(string sql, params object[] parameters);        

        void Execute(string sql, params object[] parameters);       
    }
}
