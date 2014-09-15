using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Doloco.Pages;
using DolocoApiClient;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco
{
	public static class App
	{
        static Assembly _reflectionAssembly;
	    static ILoginManager loginManager;
	    public static IMediaPicker MediaPicker;
	    public static double UserLatitude;
        public static double UserLongitude;
	    public static readonly IDolocoApiClient ApiClient;
	    public static string Token;
        internal static IDictionary<Type, Type> TypeMap;
        internal static readonly MethodInfo GetDependency;

        private static readonly Dictionary<string, string> _appEnv = new Dictionary<string, string>
        {
            {"staging", "http://dolocony.asuscomm.com:3000/api/v1"},
            {"live", "https://doloco.io/api/v1"}
        };

        static App()
        {
            ApiClient = new DolocoApiClient.DolocoApiClient(_appEnv["staging"]);
            Token = null;

			TypeMap = new Dictionary<Type, Type> 
			{};

            GetDependency = typeof(DependencyService)
                .GetRuntimeMethods()
                .Single((method) =>
                    method.Name.Equals("Get"));
        }

	    public static Page GetLoginPage(ILoginManager ilm)
	    {
	        loginManager = ilm;

            return new LoginModalPage(ilm);
	    }

        public static void Logout()
        {
            loginManager.Logout();
        }

        public static Page GetMainPage()
        {
            return new RootPage();
        }

	    public static void SetMediaPicker(IMediaPicker imp)
	    {
	        MediaPicker = imp;
	    }

        public static void Init(Assembly assembly)
        {
            System.Threading.Interlocked.CompareExchange(ref _reflectionAssembly, assembly, null);
        }
	}
}

