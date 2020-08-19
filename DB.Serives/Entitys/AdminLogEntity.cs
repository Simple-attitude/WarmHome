using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 管理员日志表
    /// </summary>
    public class AdminLogEntity:BaseEntity
    {
        public long UserId { get; set; }
        public virtual UserEntity  User { get; set; }
        public string Msg { get; set; }

    }
}
