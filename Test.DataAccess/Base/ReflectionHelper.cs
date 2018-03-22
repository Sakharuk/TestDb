using System;
using System.Reflection;
using Test.DataAccess.BaseInterfaces;

namespace Test.DataAccess.Base
{
    public class ReflectionHelper : IReflectionHelper
    {
        public void Dispose()
        {            
        }

        public Object GetPropValue(Object obj, String name)
        {
            Object result = null;
            foreach (String part in name.Split('.'))
            {
                if (obj != null)
                {
                    Type type = obj.GetType();
                    PropertyInfo info = type.GetProperty(part);
                    if (info != null)
                    {
                        result = info.GetValue(obj, null);
                    }
                }
            }
            return result;
        }
    }
}
