using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Test.DataAccess.BaseInterfaces;

namespace Test.DataAccess.Base
{
    public class BaseDbContext : DbContext, IDbContext
    {
        private IReflectionHelper _reflectionHelper;
        private IDbObjectsFactory _dbObjectFactory;
        private readonly string _connectionString;

        public BaseDbContext(IReflectionHelper reflectionHelper, IDbObjectsFactory dbObjectFactory)
            : base("name=TestDBContext")
        {
            initializeDbContext(reflectionHelper, dbObjectFactory);
            _connectionString = this.Database.Connection.ConnectionString;
        }

        public BaseDbContext(string connectionString, IReflectionHelper reflectionHelper, IDbObjectsFactory dbObjectFactory)
            : base(connectionString)
        {
            initializeDbContext(reflectionHelper, dbObjectFactory);
            _connectionString = connectionString;
        }

        public void MarkAsNew(object entity)
        {
            fixupDetachedEntities(entity);
            this.Entry(entity).State = EntityState.Added;
        }

        private void fixupDetachedEntities(object entity)
        {
            foreach (var propInfo in entity.GetType().GetProperties())
            {
                var propValue = _reflectionHelper.GetPropValue(entity, propInfo.Name);
                if (propValue != null && propValue is IEntity
                    && (this.Entry(propValue).State == EntityState.Detached))
                {
                    this.Set(propValue.GetType()).Attach(propValue);
                }
            }
        }

        private void initializeDbContext(IReflectionHelper reflectionHelper, IDbObjectsFactory dbObjectFactory)
        {
            _reflectionHelper = reflectionHelper;
            _dbObjectFactory = dbObjectFactory;
        }

        public void MarkAsModified(object entity)
        {
            if (this.Entry(entity).State != EntityState.Modified)
            {
                this.Entry(entity).State = EntityState.Modified;
            }
        }

        public void MarkAsDeleted(object entity)
        {
            this.Entry(entity).State = EntityState.Deleted;
        }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return this.Set<TEntity>();
        }

        public IEnumerable<TElement> Execute<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters).ToList();
        }
        
        public void Execute(string sql, params object[] parameters)
        {
            Database.ExecuteSqlCommand(sql, parameters);
        }

    }
}
