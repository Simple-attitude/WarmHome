using AdminUI.App_Start;
using AdminUI.Models;
using DB.ISerives;
using DB.IService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using WarmHome.Common;
using WarmHome.DTO;

namespace AdminUI.Controllers
{
    public class AdminUserController : Controller
    {
        public ILog Log = LogManager.GetLogger(nameof(AdminUserController));
        public IRoleService RoleService { get; set; }
        public IAdminUserService AdminUserService { get; set; }
        public ICityService CityService { get; set; }
        [CheckPermissions("User.List")]
        // GET: AdminUser
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        [CheckPermissions("User.List")]
        public ActionResult ListData(int page,int limit)
        {
            var count = AdminUserService.GetAll();
            var data=    count.OrderBy(e=>e.CreateDateTime).Skip((page-1)*limit).Take(limit);
            return Json(new AjaxResult<AdminUserDTO>
            {
                code = 0,
                count = count.Count(),
                data = data,
                msg = "查询成功"
            });
        }
        [HttpGet]
        [CheckPermissions("User.Add")]
        public ActionResult AddAdminUser()
        {
            var citys= CityService.GetAll().ToList();
            citys.Insert(0, new CityDTO { Id = 0, Name = "总部" });
            ViewBag.RolesAll = RoleService.GetAll();
            ViewBag.CityAll = citys;
            return View();
        }
        [HttpPost]
        [CheckPermissions("User.Add")]
        public ActionResult AddAdminUser(AdminUserAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = CommonHelper.GetValidMsg(ModelState)
                });
            }
            if (AdminUserService.GetByPhoneNum(model.PhoneNum)!=null)
            {
                return Json(new AjaxResult<object>
                {
                     code=1,
                     msg="手机号已存在"
                });
            }
            try
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    long? cityid = null;
                    if (model.City!=0)
                    {
                        cityid = model.City;
                    }
                    var id = AdminUserService.AddAdminUser(model.Name, model.PhoneNum, model.Password, model.Email,cityid);
                    AdminUserService.AddAdminUserRole(id, model.Roles);
                    sc.Complete();
                }
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "添加成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("添加管理员用户失败：{0}", ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = ex.Message
                }) ;
            }

        }
        [HttpPost]
        [CheckPermissions("CheckLoginPhoneNum")]
        public ActionResult CheckLoginPhoneNum(string phoneNum)
        {
            try
            {
               var data=  AdminUserService.GetByPhoneNum(phoneNum);
                if (data!=null)
                {
                    return Json(new AjaxResult<object>
                    {
                        code = 1,
                        msg = "此手机号已存在！"
                    });
                }
                return Json(new AjaxResult<object>
                {
                    code = 0
                }); 
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = ex.Message
                });
            }
        }
        [HttpPost]
        [CheckPermissions("User.Deleted")]
        public ActionResult Deleted(long id)
        {
            try
            {
                AdminUserService.MarkDeleted(id);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("删除失败：{0}", ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = "删除失败"
                });
            }

        }
        [HttpPost]
        [CheckPermissions("User.Deleted")]
        public ActionResult ManyDeleted(string idsStr)
        {
            try
            {
                var ids = idsStr.Substring(0, idsStr.LastIndexOf(',')).Split(',');
                foreach (var id in ids)
                {
                    AdminUserService.MarkDeleted(long.Parse(id));
                }
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("批量删除失败：{0}", ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = "删除失败"
                });
            }

        }
        [HttpGet]
        [CheckPermissions("User.Edit")]
        public ActionResult Edit(long id)
        {
            var citys = CityService.GetAll().ToList();
            citys.Insert(0, new CityDTO() { Id = 0, Name = "总部" });
            AdminUserEditViewModel model = new AdminUserEditViewModel()
            {
                User = AdminUserService.GetById(id),
                GetRolesAll = RoleService.GetAll(),
                AdminRoles = RoleService.GetAdminRoles(id),
                GetCityAll = citys
            };

            return View(model);
        }
        [HttpPost]
        [CheckPermissions("User.Edit")]
        public ActionResult Edit(AdminUserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = CommonHelper.GetValidMsg(ModelState)
                });
            }
            try
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    long? cityId = null;
                    if (model.CityId != 0)
                    {
                        cityId = model.CityId;
                    }
                    AdminUserService.UpdateAdminUser(model.Id, model.Name, model.PhoneNum, model.Password, model.Email, cityId);
                    AdminUserService.AddAdminUserRole(model.Id, model.RoleIds);

                    sc.Complete();
                    sc.Dispose();
                }
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "编辑成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("编辑管理员用户失败：{0}", ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = ex.Message
                });
            }
        }
    }
}