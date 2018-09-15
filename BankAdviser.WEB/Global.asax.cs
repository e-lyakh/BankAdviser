using BankAdviser.BLL.Infrastructure;
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
            //NinjectModule uowModule = new UowNModule();            
            //NinjectModule inquiryManagerModule = new InquiryManagerNModule();
            //NinjectModule replyManagerModule = new ReplyManagerNModule();
            //var kernel = new StandardKernel(uowModule, inquiryManagerModule, replyManagerModule);

            //NinjectModule generalModule = new NjModule();
            NinjectModule generalModule = new NjModule("DefaultConnection");
            var kernel = new StandardKernel(generalModule);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}