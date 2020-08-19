using System;
using DB.ISerives;
using DB.IService;
using DB.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           long d= new AdminUserService().AddAdminUser("江浩一", "17572696393", "jiwanyue521", "jiwanyue521@gmail.com",null);

        }
    }
}
