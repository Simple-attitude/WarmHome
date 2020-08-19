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
    public class CityController : Controller
    {
        public ICityService  CityService { get; set; }
        public ILog Log = LogManager.GetLogger(nameof(CityController));
        // GET: City
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ListData(int page,int limit)
        {
            var count = CityService.GetAll();
            var data = count.Skip((page - 1) * limit).Take(limit);
            return Json(new AjaxResult<CityDTO>
            {
                code = 0,
                msg = "查询成功",
                data = data,
                count = count.Count()
            }) ;
        }
        [HttpPost]
        public ActionResult AddCity(string name)
        {
            try
            {
                CityService.AddCity(name);
                return Json(new AjaxResult<object>
                {
                    msg = "添加成功",
                    code = 0
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("添加城市失败{0}", ex.Message);
                return Json(new AjaxResult<object>
                {
                    msg = "添加失败",
                    code = 1
                });
            }
        }
        [HttpGet]
        public ActionResult AddCity() {
            return View(); 
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var city = CityService.GetById(id);
            return View(city);
        }
        [HttpPost]
        public ActionResult Edit(long id,string name)
        {
            try
            {
                CityService.Edit(id, name);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "编辑成功"
                }) ;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("编辑失败:" + ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = "编辑失败"
                });
            }
        }
        public ActionResult Deleted(long id)
        {
            try
            {
                CityService.Deleted(id);
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
                    code = 0,
                    msg = "删除失败"
                });
            }
        }
        [HttpPost]
        public ActionResult ManyDeleted(string idsStr)
        {
            try
            {
                var ids = idsStr.Substring(0, idsStr.LastIndexOf(',')).Split(',');
                foreach (var id in ids)
                {
                    CityService.Deleted(long.Parse(id));
                }
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
                    code = 0,
                    msg = "删除失败"
                });
            }
        }
    }
}