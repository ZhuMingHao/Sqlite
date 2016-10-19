using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Sqlite
{
    public class SingletonProvider<T> where T : new()
    {
        public SingletonProvider() { } 
        public static T Instance
        {
            get
            {
                return SingletionCreator.instance;
            }
        }
        class SingletionCreator
        {
            static SingletionCreator() { }
            internal static readonly T instance = new T();

        }

    }
}

