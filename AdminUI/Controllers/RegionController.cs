using AdminUI.App_Start;
using AdminUI.Models;
using DB.ISerives;
using DB.IService;
using log4net;
using System;
using System.Linq;
using System.Web.Mvc;
using WarmHome.Common;
using WarmHome.DTO;

namespace AdminUI.Controllers
{
    public class RegionController : Controller
    {
        public IRegionService RegionService { get; set; }
        public ICityService CityService { get; set; }
        public ILog Log = LogManager.GetLogger(nameof(RegionController));

        // GET: Region
        [CheckPermissions("Region.List")]
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        [CheckPermissions("Region.List")]
        public ActionResult ListData(int page, int limit)
        {
            var count = RegionService.GetAll();
            var data = count.Skip((page - 1) * limit).Take(limit);
            return Json(new AjaxResult<RegionDTO>
            {
                code = 0,
                data = data,
                count = count.Count(),
                msg = "查询成功"
            });
        }

        [HttpGet]
        [CheckPermissions("Region.List")]
        [CheckPermissions("Region.Add")]
        public ActionResult AddRegion()
        {
            var data = CityService.GetAll().ToList();
            data.Insert(0, new CityDTO { Name = "请选择城市" });
            return View(data);
        }

        [HttpPost]
        [CheckPermissions("Region.List")]
        [CheckPermissions("Region.Add")]
        public ActionResult AddRegion(long cityId, string name)
        {
            try
            {
                RegionService.AddRegion(cityId, name);
                return Json(new AjaxResult<Object>
                {
                    code = 0,
                    msg = "添加成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("添加小区失败：" + ex.Message);
                return Json(new AjaxResult<Object>
                {
                    code = 1,
                    msg = ex.Message
                });
            }
        }

        [HttpGet]
        [CheckPermissions("Region.List")]
        [CheckPermissions("Region.Edit")]
        public ActionResult Edit(long id)
        {
            RegionViewModel model = new RegionViewModel();
            model.Region = RegionService.GetById(id);
            model.Cities = CityService.GetAll();
            return View(model);
        }

        [HttpPost]
        [CheckPermissions("Region.List")]
        [CheckPermissions("Region.Edit")]
        public ActionResult Edit(long id, long cityId, string name)
        {
            try
            {
                RegionService.UpdateRegion(id, cityId, name);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "编辑成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("编辑失败：" + ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = "编辑失败"
                });
            }
        }

        [HttpPost]
        [CheckPermissions("Region.List")]
        [CheckPermissions("Region.Deleted")]
        public ActionResult Deleted(long id)
        {
            try
            {
                RegionService.Deleted(id);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("删除区域错误：" + ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除失败"
                });
            }
        }

        [HttpPost]
        [CheckPermissions("Region.List")]
        [CheckPermissions("Region.Deleted")]
        public ActionResult ManyDeleted(string idsStr)
        {
            try
            {
                var ids = idsStr.Substring(0, idsStr.LastIndexOf(',')).Split(',');
                foreach (var id in ids)
                {
                    RegionService.Deleted(long.Parse(id));
                }
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("删除区域错误：" + ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除失败"
                });
            }
        }
    }
}