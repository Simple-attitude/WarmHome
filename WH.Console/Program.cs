using DB.Service;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WH.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                AdminUserEntity admin = new AdminUserEntity()
                {
                    Email = "1597100808@qq.com",
                    Name = "简约态度",
                    PasswordHash = "123",
                    PasswordSalt = "123",
                    PhoneNum = "13991414185",
                    LoginErrorTimes = 1,
                    LastLoginErrorDateTime = DateTime.Now
                         
                           
                };

                db.AdminUsers.Add(admin);
                db.SaveChanges();
                System.Console.WriteLine("创建成功");
            }
        }
    }
}
