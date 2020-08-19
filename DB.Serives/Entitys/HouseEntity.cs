using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 房屋配置表
    /// </summary>
    public class HouseEntity : BaseEntity
    {
        public long CommunityId { get; set; }//小区Id
        public virtual CommunityEntity Community{get; set; }
        public long RoomTypeId { get; set; }//户型类型（IdNames表）
        public virtual IdNameEntity RoomType { get; set; }

        public string Address { get; set; }//房间具体地址
        public int MonthRent { get; set; }//房屋租金
        public int TotalFloorCount { get; set; }
        public long StatusId { get; set; }//房屋状态（指向IdNames表）
        public virtual IdNameEntity Status { get; set; }

        public decimal Area { get; set; }//房屋面积
        public long DecorateStatusId { get; set; }//装修状态（指向IdNames表）
        public virtual IdNameEntity DecorateStatus { get; set; }

        public int FloorIndex { get; set; }//楼层
        public long TypeId { get; set; }//房屋类别（指向IdNames）
        public virtual  IdNameEntity  Type { get; set; }
        public string Direction { get; set; }//朝向
        public DateTime LookableDateTime { get; set; }//可看房时间
        public DateTime CheckInDateTime{ get; set; }//可入住时间
        public string OwnerName { get; set; }//业主姓名
        public string OwnerPhoneNum { get; set; }//业主电话
        public string Description { get; set; }//房间描述
        public virtual ICollection<AttachmentEntity> Attachments { get; set; } = new  List<AttachmentEntity>();//房间配置关系
        public virtual ICollection<HousepicEntity> Housepics { get; set; } = new List<HousepicEntity>();//图片配置
    }
}
