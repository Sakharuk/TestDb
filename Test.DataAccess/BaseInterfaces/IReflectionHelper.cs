using System;

namespace Test.DataAccess.BaseInterfaces
{
    public interface IReflectionHelper : IDisposable
    {
        Object GetPropValue(Object obj, String name);
    }
}