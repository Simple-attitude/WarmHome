using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.IService
{
   public  interface IAdminLogService
    {
        void AddAdminLog(long adminUserId,string message);
    }
}
