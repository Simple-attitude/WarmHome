using AdminUI.Models;
using DB.ISerives;
using DB.IService;
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
    public class CommunityController : Controller
    {
        public ICommunityService  CommunityService { get; set; }
        public IRegionService RegionService { get; set; }
        public ICityService CityService { get; set; }
        public IAdminUserService AdminUserService { get; set; }
        public ILog Log = LogManager.GetLogger(nameof(CommunityService));
        // GET: Community
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ListData(int page,int limit)
        {
            var count = CommunityService.GetAll();
            var data = count.Skip((page - 1) * limit).Take(limit);
            return Json(new AjaxResult<CommunityDTO>
            {
                code = 0,
                count = count.Count(),
                data = data
            });
        }
        [HttpGet]
        public ActionResult AddCommunity()
        {
            var userId = AdminHelper.AdminUserId(HttpContext);
            var cityId = AdminUserService.GetById(userId.Value).CityId;
            if (cityId==null)
            {
                return View("Error", (object)"总部不能添加地区");
            }
            var data = RegionService.GetCityAll((long)cityId);
            return View(data);
        }
        [HttpPost]
        public ActionResult AddCommunity(CommunityDTO community)
        {
            CommunityService.AddCommunity(community);
            return Json(new AjaxResult<object>
            {
                code = 0,
                msg = "添加成功"
            });
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var data= CommunityService.GetById(id);
            var userId = AdminHelper.AdminUserId(HttpContext);
            var cityId = AdminUserService.GetById(userId.Value).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部不能编辑地区");
            }
            CommunityEditModel model = new CommunityEditModel()
            {
                Community = data,
                Regions = RegionService.GetAll()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(CommunityDTO community)
        {
            try
            {
                CommunityService.UpdateCommunity(community);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "编辑成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("编辑小区失败：" + ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "编辑失败"
                });
            }

        }
        public ActionResult Deleted(long id)
        {
            try
            {
                CommunityService.Deleted(id);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("删除区域失败：" + ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = "删除失败"
                });
            }
        }
        public ActionResult Deleted(string idsStr)
        {
            try
            {
                var ids = idsStr.Substring(0, idsStr.LastIndexOf(',')).Split(',');
                foreach (var id in ids)
                {
                    CommunityService.Deleted(long.Parse(id));
                }
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("删除区域失败：" + ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = "删除失败"
                });
            }
        }
    }
}