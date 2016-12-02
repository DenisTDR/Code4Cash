using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;

namespace Code4Cash
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
            
            JsonContentNegotiator.JsonNegotiate();
        }
    }
}
