using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.IService
{
    public interface IAttachmentService
    {
        IEnumerable<AttachmentDTO> GetAll();
        IEnumerable<AttachmentDTO> GetHouseId(long houseID);
    }
}
