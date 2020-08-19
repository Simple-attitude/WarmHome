using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.ISerives
{
    public interface IUserService
    {
        long AddNew(String phoneNum, String password);
        UserDTO GetById(long id);
        UserDTO GetByPhoneNum(String phoneNum);
        ////检查用户名密码是否正确（很好体现了分层的思想）
        bool CheckLogin(String phoneNum, String password);
        void UpdatePwd(long userId, String newPassword);
        //设置用户 userId的城市   id
        void SetUserCityId(long userId, long cityId);

    }
}
