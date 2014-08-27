using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Doloco.Pages;
using Doloco.Repositories;
using DolocoApiClient;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco
{
	public static class App
	{
        static Assembly _reflectionAssembly;
	    static ILoginManager loginManager;
	    public static readonly IDolocoApiClient ApiClient;
	    public static string Token;
        internal static IDictionary<Type, Type> TypeMap;
        internal static readonly MethodInfo GetDependency;

        static App()
        {
            ApiClient = new DolocoApiClient.DolocoApiClient("http://dolocony.asuscomm.com:3000/api/v1");
            Token = null;

			TypeMap = new Dictionary<Type, Type> 
			{
				{typeof(Campaign), typeof(CampaignRepository)},
				{typeof(Organization), typeof(OrganizationRepository)}
			};

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

        public static void Init(Assembly assembly)
        {
            System.Threading.Interlocked.CompareExchange(ref _reflectionAssembly, assembly, null);
        }
	}
}

