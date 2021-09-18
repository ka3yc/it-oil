using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace Itoil.General
{
    /// <summary>
    /// Атрибут политики кроссдоменных запросов для ГипВН. Получает из конфига список origin и допустимых заголовков
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        public const int PreflightMaxAge = 86400;
        const string AllowedOriginsSettingName = "AllowedOriginsForCors";
        const string AllowedHeadersSettingName = "AllowedHeadersForCors";

        private CorsPolicy _policy;

        public CorsPolicyAttribute()
        {
            // Create a CORS policy
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                SupportsCredentials = true,
                PreflightMaxAge = PreflightMaxAge
            };

            //Добавим допустимые http-заголовки
            foreach (var header in GetAllowedHeadersFromConfig())
                _policy.Headers.Add(header);

            // Add allowed origins
            foreach(var origin in GetAllowedOriginsFromConfig())
                _policy.Origins.Add(origin);
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }

        /// <summary>
        /// Получить из конфигурационного файла список адресов, для которых CORS будет включен. * ставить нельзя, т.к. она не совместима с флагом SupportCredentials
        /// </summary>
        /// <returns>Строка с origin-ами через запятую</returns>
        public string GetAllowedOriginsFromConfigInString()
        {
            var setting = ConfigurationManager.AppSettings[AllowedOriginsSettingName];
            if (String.IsNullOrWhiteSpace(setting))
                throw new ConfigurationErrorsException($"В файле конфигурации, в разделе appSettings не найдена настройка {AllowedOriginsSettingName}");

            return setting;
        }

        /// <summary>
        /// Получить из конфигурационного файла список адресов, для которых CORS будет включен. * ставить нельзя, т.к. она не совместима с флагом SupportCredentials
        /// </summary>
        /// <returns></returns>
        IEnumerable<String> GetAllowedOriginsFromConfig()
        {
            var setting = GetAllowedOriginsFromConfigInString();
            return setting.Split(',').Select(s => s.Trim());
        }

        /// <summary>
        /// Получить допустимые http заголовки для CORS запроса.  Нужно из-за наличия кастомных заголовков
        /// </summary>
        /// <returns>Строка с заголовками через запятую</returns>
        public string GetAllowedHeadersFromConfigInString()
        {
            var setting = ConfigurationManager.AppSettings[AllowedHeadersSettingName];
            if (String.IsNullOrWhiteSpace(setting))
                throw new ConfigurationErrorsException($"В файле конфигурации, в разделе appSettings не найдена настройка {AllowedHeadersSettingName}");

            return setting;
        }

        /// <summary>
        /// Получить допустимые http заголовки для CORS запроса.  Нужно из-за наличия кастомных заголовков
        /// </summary>
        /// <returns></returns>
        IEnumerable<String> GetAllowedHeadersFromConfig()
        {
            var setting = GetAllowedHeadersFromConfigInString();
            return setting.Split(',').Select(s => s.Trim());
        }
    }
}