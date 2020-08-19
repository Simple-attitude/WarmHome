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
    public class HouseController : Controller
    {
        public ILog Log = LogManager.GetLogger(nameof(HouseController));
        public IHouseService HouseService { get; set; }
        public ICityService  CityService { get; set; }
        public IAdminUserService AdminUserService { get; set; }
        public IRegionService  RegionService { get; set; }
        public IIdNameService  IdNameService { get; set; }
        public IAttachmentService  AttachmentService { get; set; }
        public ICommunityService CommunityService { get; set; }
        [HttpPost]
        public ActionResult LoadCommunities(long regionId)
        {
            var communities = CommunityService.GetByRegionId(regionId);
            return Json(new AjaxResult<object> { 
                 code=0,
                 msg="查找成功",
                 data=communities,
                 count=communities.Count()
            });
        }

        // GET: House
        public ActionResult List(long typeId)
        {
            HouseListModel house = new HouseListModel();
            long id = (long)AdminHelper.AdminUserId(HttpContext);
            house.CityId = AdminUserService.GetById(id).CityId;
            house.TypeId = typeId;
            if (house.CityId == null)
            {
                return View("ERROR","总部不能进行房源管理");
            }
            return View(house);
        }
        [HttpPost]
        public ActionResult ListData(long cityId,long typeId,int page, int limit)
        {
            var count = HouseService.GetAll(cityId,typeId);
            var data= count.OrderBy(e => e.CreateDateTime).Skip((page - 1) * limit).Take(limit);
            return Json(new AjaxResult<HouseDTO>
            {
                code = 0,
                count = data.Count(),
                data = data,
                msg = "查询成功"
            }); ;
        }
        [HttpGet]
        public ActionResult AddHouse()
        {
            long? id = AdminHelper.AdminUserId(HttpContext);
            long? cityId = AdminUserService.GetById(id.Value).CityId;
            if (cityId == null)
            {
                return View("ERROR", "总部不能进行房源管理");
            }
            var regions = RegionService.GetCityAll(cityId.Value).ToList();
            regions.Insert(0, new RegionDTO() { Id = 0, Name = "请选择" });
            var roomTypes = IdNameService.GetAll("户型");
            var statuses = IdNameService.GetAll("房屋状态");
            var decorateStatuses = IdNameService.GetAll("装修状态");
            var attachments = AttachmentService.GetAll();
            var types = IdNameService.GetAll("房屋类别");
            HouseAddViewModel model = new HouseAddViewModel();
            model.Regions = regions;
            model.RoomTypes = roomTypes;
            model.Statuses = statuses;
            model.DecorateStatuses = decorateStatuses;
            model.Attachments = attachments;
            model.Types = types;
            return View(model);

        }
        [HttpPost]
        public ActionResult AddHouse(HouseAddModel model)
        {
            long? id = AdminHelper.AdminUserId(HttpContext);
            long? cityId = AdminUserService.GetById(id.Value).CityId;
            if (cityId == null)
            {
                return View("ERROR", "总部不能进行房源管理");
            }
            try
            {
                    HouseAddNewDTO dto = new HouseAddNewDTO();
                    dto.Address = model.address;
                    dto.Area = model.area;
                    dto.AttachmentIds = model.attachmentIds;
                    dto.CheckInDateTime = model.checkInDateTime;
                    dto.CommunityId = model.CommunityId;
                    dto.DecorateStatusId = model.DecorateStatusId;
                    dto.Description = model.description;
                    dto.Direction = model.direction;
                    dto.FloorIndex = model.floorIndex;
                    dto.LookableDateTime = model.lookableDateTime;
                    dto.MonthRent = model.monthRent;
                    dto.OwnerName = model.ownerName;
                    dto.OwnerPhoneNum = model.ownerPhoneNum;
                    dto.RoomTypeId = model.RoomTypeId;
                    dto.StatusId = model.StatusId;
                    dto.TotalFloorCount = model.totalFloor;
                    dto.TypeId = model.TypeId;

                    long houseId = HouseService.AddNew(dto);
                    return Json(new AjaxResult<object>
                    {
                        code = 0,
                        msg = "添加成功"
                    });
                
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("添加房源失败:{0}", ex.Message);
                return Json(new AjaxResult<string>
                {
                    code = 1,
                    msg = ex.Message
                }) ;
            }


        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            long? userId = AdminHelper.AdminUserId(HttpContext);
            long? cityId = AdminUserService.GetById(userId.Value).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部不能进行房源管理");
            }
            var house = HouseService.GetById(id);
            HouseEditViewModel model = new HouseEditViewModel();
            model.House = house;

            var regions = RegionService.GetCityAll(cityId.Value).ToList();
            //regions.Insert(0, new RegionDTO() {Id=0, Name = "请选择区域" });
            var roomTypes = IdNameService.GetAll("户型");
            var statuses = IdNameService.GetAll("房屋状态");
            var decorateStatuses = IdNameService.GetAll("装修状态");
            var attachments = AttachmentService.GetAll();
            var types = IdNameService.GetAll("房屋类别");

            model.Regions = regions;
            model.RoomTypes = roomTypes;
            model.Statuses = statuses;
            model.DecorateStatuses = decorateStatuses;
            model.Attachments = attachments;
            model.Types = types;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(HouseEditModel model)
        {
            try
            {
                    HouseDTO dto = new HouseDTO();
                    dto.Address = model.address;
                    dto.Area = model.area;
                    dto.AttachmentIds = model.attachmentIds;
                    dto.CheckInDateTime = model.checkInDateTime;
                    //有没有感觉强硬用一些不适合的DTO，有一些没用的属性时候的迷茫？
                    dto.CommunityId = model.CommunityId;
                    dto.DecorateStatusId = model.DecorateStatusId;
                    dto.Description = model.description;
                    dto.Direction = model.direction;
                    dto.FloorIndex = model.floorIndex;
                    dto.Id = model.Id;
                    dto.LookableDateTime = model.lookableDateTime;
                    dto.MonthRent = model.monthRent;
                    dto.OwnerName = model.ownerName;
                    dto.OwnerPhoneNum = model.ownerPhoneNum;
                    dto.RoomTypeId = model.RoomTypeId;
                    dto.StatusId = model.StatusId;
                    dto.TotalFloorCount = model.totalFloor;
                    dto.TypeId = model.TypeId;
                    HouseService.Update(dto);
                    return Json(new AjaxResult<object> { code = 0, msg = "编辑成功" });

            }
            catch (Exception ex)
            {
                Log.ErrorFormat("编辑失败:{0}", ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
                    msg = ex.Message
                }); ;
            }
            
        }
        [HttpPost]
        public ActionResult Deleted(long id)
        {
            try
            {
                HouseService.MarkDeleted(id);
                return Json(new AjaxResult<object>
                {
                    code = 0,
                    msg = "删除成功"
                });
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("删除房源出现异常：{0}", ex.Message);
                return Json(new AjaxResult<object>
                {
                    code = 1,
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
                    HouseService.MarkDeleted(long.Parse(id));
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
                    msg = "批量删除成功"

                });
            }

        }
    }
}