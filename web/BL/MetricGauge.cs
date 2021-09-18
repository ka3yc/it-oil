using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.BL
{
    public class MetricGauge : Metric
    {
        /// <summary>
        /// Минимально возможное значение
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Максимально возможное значение
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Значение, при котором (и ниже) считаем, что статус fail
        /// </summary>
        public int FailThreshold { get; set; }

        /// <summary>
        /// Текущее значение
        /// </summary>
        public int Value { get; set; }

        public bool IsOk()
        {
            return Value > FailThreshold;
        }

        public MetricGauge() : base()
        {
            Type = MetricTypes.Gauge;
        }

        public override DTO.Metric ToDto()
        {
            var result = base.ToDto();
            result.Min = Min;
            result.Max = Max;
            result.IsOk = IsOk();
            result.Value = Value;
            
            return result;
        }
    }
}