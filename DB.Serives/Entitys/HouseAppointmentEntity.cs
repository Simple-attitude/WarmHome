using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 房租预约
    /// </summary>
    public class HouseAppointmentEntity:BaseEntity
    {
        public long ? UserId { get; set; }
        public virtual UserEntity User { get; set; }//预约房间的用户
        public string Name { get; set; }
        public string PhoneNum { get; set; }//手机号
        public DateTime VisitDate { get; set; }
        public long HouseId { get; set; }
        public virtual HouseEntity House { get; set; }
        public string Status { get; set; }
        public long? FollowAdminUserId { get; set; }
        public virtual AdminUserEntity FollowAdminUser { get; set; }
        public DateTime? FollowDateTime { get; set; }
    }
}
