using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    public class UserEntity:BaseEntity
    {
        public string PhoneNum { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int LoginErrorTime { get; set; }
        public DateTime? LastLoginErrorTime { get; set; }
        public long ? CityId { get; set; }
        public virtual CityEntity City { get; set; }
    }
}
