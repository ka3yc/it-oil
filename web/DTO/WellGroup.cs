using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.DTO
{
    public class WellGroup
    {
        public string Name { get; set; }

        public List<WellGroup> Children { get; set; }

        public List<Well> Wells { get; set; }
    }
}