using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    public class HousepicEntity:BaseEntity
    {
        public long HouseId { get; set; }
        public virtual HouseEntity House { get; set; }//一个房屋对应多个图片
        public string Url { get; set; }//图片地址
        public string ThumbUrl { get; set; }//缩略图地址
    }
}
