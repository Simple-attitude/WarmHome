using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 字典表，用于存储少量数据
    /// </summary>
    public class IdNameEntity:BaseEntity
    {
        public string TypeName { get; set; }//类型名称  一个类型对应多个Name
        public string Name { get; set; }//名称
    }
}
