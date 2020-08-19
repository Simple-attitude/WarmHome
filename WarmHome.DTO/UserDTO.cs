using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarmHome.DTO
{
    public class UserDTO : BaseDTO
    {
        public string PhoneNum { get; set; }
        public int LoginErrorTime { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
        public long? CityId { get; set; }
    }

}
