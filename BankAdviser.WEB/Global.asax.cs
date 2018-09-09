using BankAdviser.BLL.Infrastructure;
using BankAdviser.WEB.Util;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BankAdviser.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Dependencies injection:            
            NinjectModule uowModule = new NinjectUowModule();
            NinjectModule enquiryManagerModule = new NinjectEmMolule();
            NinjectModule replyManagerModule = new NinjectRemMolule();
            var kernel = new StandardKernel(uowModule, enquiryManagerModule, replyManagerModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}