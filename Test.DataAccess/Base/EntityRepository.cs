using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Test.DataAccess.BaseInterfaces;

namespace Test.DataAccess.Base
{
    public class EntityRepository<ET> : ContextRepository, IEntityRepository<ET>
         where ET : class
    {
        public EntityRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public virtual ET Get<TId>(TId id)
        {
            var entityFromDb = dbContext.Set<ET>().Where(buildEntityIdEqualsExpression(id)).FirstOrDefault();
            return entityFromDb;
        }

        public virtual ET GetDetached<TId>(TId id)
        {
            var entityFromDb = dbContext.Set<ET>().AsNoTracking().Where(buildEntityIdEqualsExpression(id)).FirstOrDefault();
            return entityFromDb;
        }

        private Expression<Func<ET, bool>> buildEntityIdEqualsExpression<TId>(TId id)
        {
            ParameterExpression entityPrmExpr = Expression.Parameter(typeof(ET), "entity");
            ConstantExpression idValueExpr = Expression.Constant(id, typeof(TId));
            BinaryExpression entityIdEqualIdExpr = Expression.Equal(Expression.Property(entityPrmExpr, getIdPropertyName()), idValueExpr);
            Expression<Func<ET, bool>> whereIdEqualsExpr = Expression.Lambda<Func<ET, bool>>(entityIdEqualIdExpr, new ParameterExpression[] { entityPrmExpr });
            return whereIdEqualsExpr;
        }

        public virtual IEnumerable<ET> GetAll()
        {
            IEnumerable<ET> list = getAll().ToList();
            return list;
        }

        public virtual IEnumerable<ET> GetAllDetached()
        {
            IEnumerable<ET> list = getAll().AsNoTracking().ToList();
            return list;
        }

        public virtual void Create(ET entity)
        {
            dbContext.MarkAsNew(entity); 
        }

        public void Update(ET entity)
        {
            dbContext.MarkAsModified(entity); 
        }

        public void Delete(int id)
        {
            var entityToDelete = this.Get(id); 
            dbContext.MarkAsDeleted(entityToDelete); 
        }

        private IQueryable<ET> getAll()
        {
            return this.dbContext.Set<ET>();
        }
        private string getIdPropertyName()
        {
            string propName = string.Empty;
            PropertyInfo[] props = typeof(ET).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    KeyAttribute authAttr = attr as KeyAttribute;
                    if (authAttr != null)
                    {
                        propName = prop.Name;
                    }
                }
            }
            return propName;
        }

        public void Delete(ET entity)
        {
            dbContext.MarkAsDeleted(entity);
        }
    }
}
