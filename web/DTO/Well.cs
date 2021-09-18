using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.DTO
{
    public class Well
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public bool IsInternetOk { get; set; }

        public int SignalLevel { get; set; }

        public double Latitude { get; set; }
        
        public double Longitude { get; set; }

        public DateTime LastStatusUpdate { get; set; }
    }
}