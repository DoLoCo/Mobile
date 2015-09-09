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
	    public static IEnumerable<Campaign> NearbyCampaigns; 
	    public static readonly IDolocoApiClient ApiClient;
	    public static User CurrentUser;
	    public static string Token;
        internal static IDictionary<Type, Type> TypeMap;
        internal static readonly MethodInfo GetDependency;

	    public const String CurrentAppEnv = "staging";
        public static readonly Dictionary<string, string> AppEnv = new Dictionary<string, string>
        {
            {"staging", ""},
            {"live", ""}
        };

        static App()
        {
            ApiClient = new DolocoApiClient.DolocoApiClient(String.Format("{0}/api/v1", AppEnv[CurrentAppEnv]));
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

