using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Fixtures.Attributes
{
    public class DispatcherPool
    {
        private Dictionary<string, Lazy<Dispatcher>> _dispatchers = new Dictionary<string, Lazy<Dispatcher>>();

        public DispatcherPool()
        {
            AddDispatchers(Assembly.GetAssembly(typeof(DispatcherPool)));
        }

        public Dispatcher this[string dispacherName]
        {
            get
            {
                if (_dispatchers.ContainsKey(dispacherName))
                {
                    return _dispatchers[dispacherName].Value;
                }
                return null;
            }
        }

        private void AddDispatchers(Assembly assembly)
        {
            assembly.GetTypes()
                .Where(x=>x.IsSubclassOf(typeof(Dispatcher)))
                .ToList()
                .ForEach(t =>
                {
                    var dispatcher = new Lazy<Dispatcher>(() => Activator.CreateInstance(t) as Dispatcher);
                    _dispatchers.Add(t.Name, dispatcher);
                    t.GetCustomAttributesData()
                        .Where(a => a.AttributeType == typeof (AliasAttribute))
                        .Select(a => a.ConstructorArguments.FirstOrDefault())
                        .ToList()
                        .ForEach(a =>
                        {
                            _dispatchers.Add(a.Value.ToString(), dispatcher);
                        });

                });
        }
    }
}