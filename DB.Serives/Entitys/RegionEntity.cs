using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    public class RegionEntity:BaseEntity
    {
        public string Name { get; set; }//名称
        public long CityId { get; set; }//外键
        public virtual CityEntity City { get; set; }//一个地区对应一个城市
    }
}
