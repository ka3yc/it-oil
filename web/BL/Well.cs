using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.BL
{
    public class Well
    {
        public int Id { get; set; }

        public string OwnerName { get; set; }

        public string Field { get; set; }

        public string Correlation { get; set; }

        public string WellPad { get; set; }

        public string Name { get; set; }

        public bool IsInternetOk { get; set; }

        public int SignalLevel { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime LastStatusUpdate { get; set; }

        public DTO.Well ToDto()
        {
            return new DTO.Well
            {
                Id = Id,
                IsInternetOk = IsInternetOk,
                LastStatusUpdate = LastStatusUpdate,
                Latitude = Latitude,
                Longitude = Longitude,
                Name = Name,
                FullName = $"{Field}, {Correlation}, {WellPad}, {Name}",
                SignalLevel = SignalLevel
            };
        }
    }
}