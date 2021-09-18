using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.DTO
{
    public class MetricHistory
    {
        public string Name { get; set; }

        public int FaultCount { get; set; }

        public List<MetricHistoryItem> History { get; set; }
    }
}