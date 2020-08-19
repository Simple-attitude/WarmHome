using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 小区表
    /// </summary>
    public class CommunityEntity:BaseEntity
    {
        public string Name { get; set; }//名称
        public long RegionId { get; set; }//地区Id
        public virtual RegionEntity  Region { get; set; }//一个地区对应多个小区
        public string Location { get; set; }//小区的地址
        public string Traffic { get; set; }//公交路线
        public int ?BuiltYear { get; set; }//小区建造年份
    }
}
