using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmHome.DTO;

namespace AdminUI.Models
{
    public class RegionViewModel
    {
        public RegionDTO   Region { get; set; }
        public IEnumerable<CityDTO>  Cities { get; set; }
    }
}