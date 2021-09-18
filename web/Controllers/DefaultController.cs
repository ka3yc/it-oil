using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Itoil.Controllers
{
    [RoutePrefix("")]
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
    public class DefaultController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(@"<h1>Вас приветствует сайт Webapi сайта мониторинга параметров ЦУСС!</h1> <a href='view/'>Сайт</a>", System.Text.Encoding.Default)
            };
            
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");

            return result;
        }
    }
}
