using System.Collections.Generic;

namespace Test.DataAccess.BaseInterfaces
{
    public interface IEntityRepository<ET> where ET : class
    {
        void Create(ET entity);
        void Delete(int id);
        void Delete(ET entity);
        ET Get<TId>(TId id);
        ET GetDetached<TId>(TId id);
        IEnumerable<ET> GetAll();
        IEnumerable<ET> GetAllDetached();
        void Update(ET entity);
    }
}
