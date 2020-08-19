using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 管理员用户表
    /// </summary>
    public class AdminUserEntity : BaseEntity {
        public string Name { get; set; }//名称
        public string PhoneNum { get; set; }//手机号
        public string PasswordSalt { get; set; }//密码盐
        public string PasswordHash { get; set; }//密码Md5值
        public string Email { get; set; }//电子邮箱
        public long ?CityId { get; set; }//管理员所属城市   cityid为空则表式总部员工
        public virtual CityEntity City { get; set; }
        public int LoginErrorTimes { get; set; }//错误登录次数
        public DateTime ? LastLoginErrorDateTime { get; set; }//最后一次登录错误时间
        public virtual ICollection<RolesEntity> Roles { get; set; } = new List<RolesEntity>();//一个用户对应多个角色

    }
}
