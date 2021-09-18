
using System.Web.Http;
using System.Web.Http.Cors;

namespace Itoil
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Включение CORS. для применения настроек, нужно на класс контроллера навесить атрибут GipvnCorsPolicy
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new LogExceptionFilterAttribute());
        }
    }
}
