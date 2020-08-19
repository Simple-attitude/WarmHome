using AdminUI.App_Start;
using DB.ISerives;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WarmHome.Common;
using WarmHome.DTO;

namespace AdminUI.Controllers
{
    public class PermissionController : Controller
    {
        public ILog Log = LogManager.GetLogger(nameof(PermissionController));
        public IPerissionService PerissionService { get; set; }
        // GET: Permission
        [CheckPermissions("Permission.List")]
        public ActionResult List()
        {
            return View();
        }
        //[HttpPost]
        [HttpGet]
        [CheckPermissions("Permission.List")]
        public ActionResult ListData(int page, int limit)
        {
            var count = PerissionService.GetAll();
             var data=   count.OrderBy(e => e.Id).Skip((page - 1) * limit).Take(limit);
            return Json(new AjaxResult<PermissionDTO>
            {
                code = 0,
                msg = "加载成功",
                count = count.Count(),
                data = data
            });
        }
        [HttpGet]
        [CheckPermissions("Permission.Add")]
        public ActionResult AddPermission()
        {
            return View();
        }
        [HttpPost]
        [CheckPermissions("Permission.Add")]
        public ActionResult AddPermission(string name,string desc)
        {
            try
            {
                PerissionService.AddPermission(name, desc);
                return Json(new AjaxResult<PermissionDTO>
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
                 return Json(new AjaxResult<PermissionDTO>
                    {
                        code = 1,
                        count = 0,
                        data = null,
                        msg = "此权限已存在"
                    });
            }

        }
        [HttpPost]
        [CheckPermissions("Permission.Deleted")]
        public ActionResult Deleted(long id)
        {         
                PerissionService.Deleted(id);
                return Json(new AjaxResult<PermissionDTO>()
                {
                    code = 0,
                    count = 0,
                    msg = "删除成功",
                    data = null
                });            
        }
        [HttpPost]
        [CheckPermissions("Permission.Deleted")]
        public ActionResult ManyDeleted(string idsStr)
        {
            var ids= idsStr.Substring(0, idsStr.LastIndexOf(',')).Split(',').ToList();
            foreach (var id in ids)
            {
                PerissionService.Deleted(long.Parse(id));
            }
            return Json(new AjaxResult<PermissionDTO>()
            {
                code = 0,
                count = 0,
                msg = "删除成功",
                data = null
            });
        }
        [HttpGet]
        [CheckPermissions("Permission.Edit")]
        public ActionResult Edit(long id)
        {
            var data = PerissionService.GetById(id);
            return View(data);
        }
        [HttpPost]
        [CheckPermissions("Permission.Edit")]
        public ActionResult Edit(long id,string name,string desc)
        {
            
            try
            {
                PerissionService.Edit(id, name, desc);
                return Json(new AjaxResult<PermissionDTO> { code = 0, count = 1, data = null, msg = "修改成功" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult<PermissionDTO> { code = 1, count = 1, data = null, msg = ex.Message });
            }
        }
    }
}