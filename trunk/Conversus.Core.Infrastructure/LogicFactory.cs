using System;
using System.Collections.Generic;

namespace Conversus.Core.Infrastructure
{
    public abstract class LogicFactory
    {
        private Dictionary<Type, object> _objectsDict;

        public void RegisterObjects(Dictionary<Type, object> objects)
        {
            _objectsDict = objects;
        }

        public T Get<T>()
        {
            return (T)_objectsDict[typeof(T)];
        }
    }
}
