using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Doloco
{
	public static class App
	{
        static Assembly _reflectionAssembly;
        internal static IDictionary<Type,Type> TypeMap;
        internal static readonly MethodInfo GetDependency;

        static App()
        {
            TypeMap = new Dictionary<Type, Type>{};

            GetDependency = typeof(DependencyService)
                .GetRuntimeMethods()
                .Single((method)=>
                    method.Name.Equals("Get"));
        }

        public static void Init(Assembly assembly)
        {
            System.Threading.Interlocked.CompareExchange(ref _reflectionAssembly, assembly, null);
        }

        public static Stream LoadResource(String name)
        {
            return _reflectionAssembly.GetManifestResourceStream(name);
        }
	}
}

