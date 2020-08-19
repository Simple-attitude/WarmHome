using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmHome.DTO;

namespace AdminUI.Models
{
    public class HouseEditViewModel
    {

            public IEnumerable<RegionDTO> Regions { get; set; }
            public IEnumerable<IdNameDTO> RoomTypes { get; set; }
            public IEnumerable<IdNameDTO> Statuses { get; set; }
            public IEnumerable<IdNameDTO> DecorateStatuses { get; set; }
            public IEnumerable<IdNameDTO> Types { get; set; }
            public IEnumerable<AttachmentDTO> Attachments { get; set; }
        public HouseDTO House { get; set; }

    }
}