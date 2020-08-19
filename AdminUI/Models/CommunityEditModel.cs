using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmHome.DTO;

namespace AdminUI.Models
{
    public class CommunityEditModel
    {
        public CommunityDTO  Community { get; set; }
        public IEnumerable<RegionDTO> Regions { get; set; }
    }
}