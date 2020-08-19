using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.IService
{
    public interface IAdminUserService
    {
        bool CheckLogin(string phoneNum, string password);
        long AddAdminUser(string name,string phoneNum,string password,string email,long ?cityId);
        void UpdateAdminUser(long id,string name, string phoneNum, string password, string email, long? cityId);
        IEnumerable<AdminUserDTO> GetAll(long ? cityId);
        IEnumerable<AdminUserDTO> GetAll();
        AdminUserDTO GetById(long id);
        AdminUserDTO GetByPhoneNum(string phoneNum);
        void MarkDeleted(long id);
        bool HasPermission(long adminUserId, string permissionName);
        void RecordLoginError(long id);//记录错误登录一次
        void ResetLoginError(long id);//重置登录错误信息

        void AddAdminUserRole(long id, long[] roleId);


    }
}
