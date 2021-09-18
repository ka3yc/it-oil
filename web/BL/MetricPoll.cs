using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.BL
{
    public class MetricPoll : Metric
    {
        /// <summary>
        /// Результат последнего опроса ресурса
        /// </summary>
        public bool IsOk { get; set; }

        /// <summary>
        /// Период опроса в секундах
        /// </summary>
        public int Period { get; set; }

        public MetricPoll() : base()
        {
            Type = MetricTypes.Poll;
        }

        public override DTO.Metric ToDto()
        {
            var result = base.ToDto();

            result.IsOk = ValueDate >= DateTime.Now.AddSeconds(-Period);
            result.PollPeriod = $"{Period} сек";
            
            return result;
        }
    }
}