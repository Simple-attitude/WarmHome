using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.ISerives
{
    public interface IHouseAppointmentService
    {
        //新增一个预约：userId用户 id（可以为 null，表示匿名用户）；name姓名、phoneNum
        //手机号、houseId房间 id、visiteDate预约看房时间
        long AddHouseAppointment(long? userid, string name, string phoneNum, long houseId, DateTime visiteDate);
        bool Follow(long adminUserId, long houseAppointmentId);
        HouseAppointmentDTO GetById(long id);
        long GetTotalCount(long cityId, String status);
        IEnumerable<HouseAppointmentDTO> GetPagedData(long cityId, String status, int pageSize, int
currentIndex);

    }
}
