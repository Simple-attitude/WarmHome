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
    public class AttachmentEntity:BaseEntity
    {
        public string Name { get; set; }//名称
        public string IconName { get; set; }//图片名称
        public ICollection<HouseEntity>  Houses { get; set; }//一个配置对应多个房子
    }
}
