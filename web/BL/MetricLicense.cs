using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.BL
{
    public class MetricLicense : Metric
    {
        public int Max { get; set; }

        public int Value { get; set; }

        public int FailThreshold { get; set; }

        public MetricLicense() : base()
        {
            Type = MetricTypes.License;
        }

        public bool IsOk()
        {
            return Value > FailThreshold;
        }

        public override DTO.Metric ToDto()
        {
            var result = base.ToDto();
            result.Value = Value;
            result.Max = Max;

            result.IsOk = IsOk();

            return result;
        }
    }
}