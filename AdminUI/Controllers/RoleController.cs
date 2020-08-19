using AdminUI.Models;
using DB.ISerives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using WarmHome.Common;
using System.Transactions;
using WarmHome.DTO;
using log4net;
using AdminUI.App_Start;

namespace AdminUI.Controllers
{
    public class RoleController : Controller
    {
        public ILog Log = LogManager.GetLogger(nameof(RoleController));
        public IRoleService RoleService { get; set; }
        public IPerissionService PerissionService { get; set; }
        [CheckPermissions("Role.List")]
        // GET: Role
        public ActionResult List()
        {
            return View();
        }
        [CheckPermissions("Role.List")]
        [HttpPost]
        public ActionResult ListData(int page, int limit)
        {
            var count = RoleService.GetAll();
            var data= count.OrderBy(e => e.CreateDateTime).Skip((page - 1) * limit).Take(limit);
            return Json(new AjaxResult<RoleDTO>()
            {
                code = 0,
                count = count.Count(),
                data = data,
                msg = "查询成功"
            });
        }
        [HttpGet]
        [CheckPermissions("Role.Add")]
        //[CheckPermissions("User.edit")]
        public ActionResult AddRole()
        {
            var pers = PerissionService.GetAll();
            return View(pers);
        }
        [CheckPermissions("Role.Add")]
        [HttpPost]
        public ActionResult AddRole(RoleAddViewModel model)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var roleId = RoleService.AddRole(model.Name);
                    PerissionService.AddPermissions(roleId, model.Permissions);
                    scope.Complete();
                }
                return Json(new AjaxResult<RoleDTO>
                {
                    code = 0,
                    count = 0,
                    data = null,
                    msg = "添加成功"
                });

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Json(new AjaxResult<RoleDTO>
                {
                    code = 1,
                    count = 0,
                    data = null,
                    msg = ex.Message
                });
            }
        }
        [CheckPermissions("Role.Deleted")]
        [HttpPost]
        public ActionResult Deleted(int id)
        {
            try
            {
                RoleService.MarkDelted(id);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    count = 0,
                    msg = "删除成功",
                    data = null
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    count = 0,
                    msg = ex.Message,
                    data = null
                }); 
            }

        }
        [CheckPermissions("Role.Deleted")]
        [HttpPost]
        public ActionResult ManyDeleted(string idsStr)
        {
            try
            {
                var ids = idsStr.Substring(0, idsStr.LastIndexOf(',')).Split(',');
                foreach (var id in ids)
                {
                    RoleService.MarkDelted(long.Parse(id));
                }
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return Json(new AjaxResult<object>
                {
                    code = 1,
                    count = 0,
                    msg = ex.Message,
                    data = null
                });
            }

        }
        [CheckPermissions("Role.Edit")]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            RoleEditViewModel role = new RoleEditViewModel() {
                Role = RoleService.GetById(id),
                RolePermissions = PerissionService.GetRoleId(id),
                AllPermissions = PerissionService.GetAll()
            };
            return View(role);
        }
        [CheckPermissions("Role.Edit")]
        [HttpPost]
        public ActionResult Edit(long roleId,string name, long[] Permissions)
        {
            try
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    RoleService.UpdataRole(roleId, name);
                    PerissionService.UpdatePermissions(roleId, Permissions);
                    sc.Complete();
                }
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    count = 0,
                    data = null,
                    msg = "修改成功"
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    count = 0,
                    data = null,
                    msg = ex.Message
                });
            }


        }
    }
}