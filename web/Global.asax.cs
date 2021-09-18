using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Itoil
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Вымученный код, надерганный из разных источников
            //Нужен для нормального прохождения т.н preflight запросов (термин в CORS)
            //Preflight запросы генерятся браузером в некоторых случаях, например, если клиент передает кастомный http-Заголовок
            //используется глагол OPTIONS. При этом браузер к этому запросу не прикладывает инфу об аутентификации
            //Настройка CORS для остальных методов (GET,POST) - в классе GipvnCorsPolicyAttribute
            //Допустимые заголовки и origins вынес в конфиг, чтоб не перекомпилировать, если нужно будет добавить
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                var corsPolicies = new General.CorsPolicyAttribute();
                var origin = HttpContext.Current.Request.Headers["ORIGIN"];
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "*");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin);
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", corsPolicies.GetAllowedHeadersFromConfigInString());
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", General.CorsPolicyAttribute.PreflightMaxAge.ToString());
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");

                HttpContext.Current.Response.AddHeader("Vary", "Accept-Encoding, Origin");
                HttpContext.Current.Response.AddHeader("Keep-Alive", "timeout=2, max=100");
                HttpContext.Current.Response.AddHeader("Connection", "Keep-Alive");

                HttpContext.Current.Response.StatusCode = 204;
                var httpApplication = sender as HttpApplication;
                httpApplication.CompleteRequest();
            }
        }
    }
}
