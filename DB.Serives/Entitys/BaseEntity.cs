using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 公共父类
    /// </summary>
    public abstract class BaseEntity
    {
        public long Id { get; set; }//主键

        public bool IsDeleted { get; set; } = false;//软删除

        public DateTime CreateDateTime { get; set; } = DateTime.Now;//创建时间
    }
}
