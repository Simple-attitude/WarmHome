using DB.IService;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service
{
     class AdminLogSerives : IAdminLogService
    {
        public void AddAdminLog(long adminUserId, string message)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                AdminLogEntity adminLog = new AdminLogEntity() { 
                  Msg=message,
                  UserId=adminUserId
                };
                db.AdminLogs.Add(adminLog);
            }
        }
    }
}
