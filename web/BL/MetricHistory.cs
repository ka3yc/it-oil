using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.BL
{
    public class MetricHistory
    {
        public DateTime Date { get; set; }

        public double Value { get; set; }

        public virtual Metric Metric { get; set; }
    }
}