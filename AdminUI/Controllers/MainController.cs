using AdminUI.Models;
using CaptchaGen;
using DB.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using WarmHome.Common;

namespace AdminUI.Controllers
{
    public class MainController : Controller
    {
        public IAdminUserService AdminUserService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = CommonHelper.GetValidMsg(ModelState)
                }) ;
            }
            if ((string)TempData["code"]!=model.Captcha)
            {
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = "验证码错误"
                });
            }
            bool result= AdminUserService.CheckLogin(model.PhoneNum, model.Password);
            if (result)
            {
                Session["AdminUserId"] = AdminUserService.GetByPhoneNum(model.PhoneNum).Id;
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "登陆成功"
                });
            }
            else
            {
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = "用户名或密码错误"
                });
            }
        }
        public ActionResult CreateCaptcha()
        {
            string code = CommonHelper.CreateVerifyCode(4);
            //MemoryStream ms = ImageFactory.GenerateImage(code, 35, 100, 9, 6);
            MemoryStream ms = ImageFactory.GenerateImage(code, 60, 150, 20, 6);
            TempData["code"] = code;
            return File(ms, "image/jpeg");
        }
    }
}