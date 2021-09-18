using Itoil.BL;
using Itoil.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Itoil.Controllers
{
    [Itoil.General.CorsPolicy()]
    [RoutePrefix("api")]
    public class CussController : ApiController
    {
        [Route("common")]
        public BaseResult<CommonData> GetCommon()
        {
            return BaseResult<CommonData>.Success(new CommonData { UserName = "Иванов И.И." });
        }

        [Route("metric")]
        public BaseResult<List<DTO.Metric>> GetMetrics()
        {
            //if (DateTime.Now.Second % 2 == 0)
            //    throw new Exception("Какая-то ошибка");

            using (var svc = new MetricService())
                return svc.GetMetrics();
        }

        [Route("well")]
        public BaseResult<List<DTO.WellGroup>> GetWells()
        {
            using (var svc = new MetricService())
                return svc.GetWellData();
        }

        /// <summary>
        /// Получение аргументов через Query String. Ожидаем запрос вида: system/test/query?id=&text=
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [Route("metric/{id}/{period}")]
        public BaseResult<DTO.MetricHistory> GetMetricHistory(int id, string period)
        {
            return BaseResult<DTO.MetricHistory>.Success(new DTO.MetricHistory
            {
                Name = $"Метрика #{id}",
                FaultCount = 2,
                History = new List<MetricHistoryItem>
                {
                    new MetricHistoryItem{ Label = "11:00", Value = "80"},
                    new MetricHistoryItem{ Label = "11:10", Value = "85"},
                    new MetricHistoryItem{ Label = "11:20", Value = "90"},
                    new MetricHistoryItem{ Label = "11:30", Value = "87"},
                    new MetricHistoryItem{ Label = "11:40", Value = "68"},
                    new MetricHistoryItem{ Label = "11:50", Value = "60"},
                    new MetricHistoryItem{ Label = "12:00", Value = "40"},
                    new MetricHistoryItem{ Label = "12:10", Value = "10"},
                    new MetricHistoryItem{ Label = "12:20", Value = "80"},
                    new MetricHistoryItem{ Label = "12:30", Value = "80"},
                    new MetricHistoryItem{ Label = "12:40", Value = "5"},
                    new MetricHistoryItem{ Label = "12:50", Value = "7"},
                    new MetricHistoryItem{ Label = "13:00", Value = "50"},
                    new MetricHistoryItem{ Label = "13:10", Value = "70"},
                    new MetricHistoryItem{ Label = "13:20", Value = "100"}
                }
            });
        }

        //[Route("metric/{id}/{value}")]
        //[HttpPost]
        //public BaseResult ReportMetricValue(int metricId, string value)
        //{

        //}

    }
}
