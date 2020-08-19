using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 配置文件表
    /// </summary>
    public class SettingEntity:BaseEntity
    {
        public string Name { get; set; }//名称
        public string Value { get; set; }
    }
}
