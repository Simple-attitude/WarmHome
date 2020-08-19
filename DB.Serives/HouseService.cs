using DB.ISerives;
using DB.Service;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.Serives
{
    class HouseService : IHouseService
    {
        public long AddNew(HouseAddNewDTO house)
        {
            HouseEntity entity = new HouseEntity();
            entity.Address = house.Address;
            entity.Area = house.Area;
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<AttachmentEntity> service = new BaseService<AttachmentEntity>(db);
                var atts = service.GetAll().Where(e => house.AttachmentIds.Contains(e.Id));
                foreach (var att in atts)
                {
                    entity.Attachments.Add(att);
                }
                entity.CheckInDateTime = house.CheckInDateTime;
                entity.CommunityId = house.CommunityId;
                entity.DecorateStatusId = house.DecorateStatusId;
                entity.Description = house.Description;
                entity.Direction = house.Direction;
                entity.FloorIndex = house.FloorIndex;
                //houseEntity.HousePics 新增后再单独添加
                entity.LookableDateTime = house.LookableDateTime;
                entity.MonthRent = house.MonthRent;
                entity.OwnerName = house.OwnerName;
                entity.OwnerPhoneNum = house.OwnerPhoneNum;
                entity.RoomTypeId = house.RoomTypeId;
                entity.StatusId = house.StatusId;
                entity.TotalFloorCount = house.TotalFloorCount;
                entity.TypeId = house.TypeId;
                db.Houses.Add(entity);
                db.SaveChanges();
                return entity.Id;

            }
        }

        public long AddNewHousePic(HousePicDTO housePic)
        {
            using (WarmHomeContext db=new  WarmHomeContext())
            {
                HousepicEntity entity = new HousepicEntity()
                {
                      ThumbUrl=housePic.ThumbUrl,
                       Url=housePic.Url,
                        HouseId=housePic.HouseId
                };
                db.Housepics.Add(entity);
                db.SaveChanges();
                return entity.Id;
            }
        }

        public void DeleteHousePic(long housePicId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                HousepicEntity entity = new HousepicEntity();
                entity.Id = housePicId;
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public HouseDTO GetById(long id)
        {
            using (WarmHomeContext db= new WarmHomeContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(db);
                var data= service.GetAll().Include(e => e.Community.Region)
                                    .Include(e => e.Community)
                                    .Include(e => e.Housepics)
                                    .Include(e => e.Community.Region.City)
                                    .Include(e => e.RoomType)
                                    .Include(e => e.Status)
                                    .Include(e => e.DecorateStatus)
                                    .Include(e => e.Type)
                                    .Include(e => e.Attachments)
                                    .AsNoTracking()
                                    .SingleOrDefault(e => e.Id == id);
                if (data==null)
                {
                    return null;
                }
                else
                {
                   return ToDTO(data);
                }
            }
        }
        private HouseDTO ToDTO(HouseEntity entity)
        {
            HouseDTO dto = new HouseDTO();
            dto.Address = entity.Address;
            dto.Area = entity.Area;
            dto.AttachmentIds = entity.Attachments.Select(a=>a.Id).ToArray();
            dto.CheckInDateTime = entity.CheckInDateTime;
            dto.CityId = entity.Community.Region.CityId;
            dto.CityName = entity.Community.Region.City.Name;
            dto.CommunityBuiltYear = entity.Community.BuiltYear;
            dto.CommunityId = entity.CommunityId;
            dto.CommunityLocation = entity.Community.Location;
            dto.CommunityName = entity.Community.Name;
            dto.CommunityTraffic = entity.Community.Traffic;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.DecorateStatusId = entity.DecorateStatusId;
            dto.DecorateStatusName = entity.DecorateStatus.Name;
            dto.Description = entity.Description;
            dto.Direction = entity.Direction;
            var firstPic = entity.Housepics.FirstOrDefault();
            if (firstPic != null)
            {
                dto.FirstThumbUrl = firstPic.ThumbUrl;
            }
            dto.FloorIndex = entity.FloorIndex;
            dto.Id = entity.Id;
            dto.LookableDateTime = entity.LookableDateTime;
            dto.MonthRent = entity.MonthRent;
            dto.OwnerName = entity.OwnerName;
            dto.OwnerPhoneNum = entity.OwnerPhoneNum;
            dto.RegionId = entity.Community.RegionId;
            dto.RegionName = entity.Community.Region.Name;
            dto.RoomTypeId = entity.RoomTypeId;
            dto.RoomTypeName = entity.RoomType.Name;
            dto.StatusId = entity.StatusId;
            dto.StatusName = entity.Status.Name;
            dto.TotalFloorCount = entity.TotalFloorCount;
            dto.TypeId = entity.TypeId;
            dto.TypeName = entity.Type.Name;
            return dto;
        }
        public int GetCount(long cityId, DateTime startDateTime, DateTime endDateTime)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(db);
                return service.GetAll().Where(e => e.Community.Region.CityId == cityId && e.CreateDateTime >= startDateTime && e.CreateDateTime <= endDateTime).Count();
            }
        }

        public IEnumerable<HouseDTO> GetPagedData(long cityId, long typeId, int pageSize, int currentIndex)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(db);
                var data = service.GetAll().Include(e => e.Community.Region)
                                    .Include(e => e.Community)
                                    .Include(e => e.Housepics)
                                    .Include(e => e.Community.Region.City)
                                    .Include(e => e.RoomType)
                                    .Include(e => e.Status)
                                    .Include(e => e.DecorateStatus)
                                    .Include(e => e.Type)
                                    .AsNoTracking()
                                    .Where(e => e.Community.Region.CityId == cityId && e.TypeId == typeId)
                                    .OrderBy(e => e.CreateDateTime)
                                    .Skip(pageSize)
                                    .Take(currentIndex);
                return data.ToList().Select(item => ToDTO(item));
            }
        }

        public IEnumerable<HousePicDTO> GetPics(long houseId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HousepicEntity> service = new BaseService<HousepicEntity>(db);
                return service.GetAll()
                                        .Where(e => e.HouseId == houseId)
                                        .ToList()
                                        .Select(item => new HousePicDTO()
                                        {
                                            Id = item.Id,
                                            HouseId = item.HouseId,
                                            ThumbUrl = item.ThumbUrl,
                                            Url = item.Url,
                                            CreateDateTime = item.CreateDateTime

                                        });
            }
        }

        public long GetTotalCout(long cityId, long typeId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(db);
                return  service.GetAll().Where(e => e.Community.Region.CityId == cityId && e.TypeId == typeId).LongCount();
            }
        }

        public void MarkDeleted(long id)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(db);
                service.MarkDeleted(id);
            }
        }

        public HouseSearchResult Search(HouseSearchOptions options)
        {
            using (WarmHomeContext ctx = new WarmHomeContext())
            {
                BaseService<HouseEntity> bs = new BaseService<HouseEntity>(ctx);
                var items = bs.GetAll().Where(h => h.Community.Region.CityId == options.CityId
                            && h.TypeId == options.TypeId);
                if (options.RegionId != null)
                {
                    items = items.Where(t => t.Community.RegionId == options.RegionId);
                }
                if (options.StartMonthRent != null)
                {
                    items = items.Where(t => t.MonthRent >= options.StartMonthRent);
                }
                if (options.EndMonthRent != null)
                {
                    items = items.Where(t => t.MonthRent <= options.EndMonthRent);
                }
                if (options.EndMonthRent != null)
                {
                    items = items.Where(t => t.MonthRent <= options.EndMonthRent);
                }
                if (!string.IsNullOrEmpty(options.Keywords))
                {
                    items = items.Where(t => t.Address.Contains(options.Keywords)
                            || t.Description.Contains(options.Keywords)
                            || t.Community.Name.Contains(options.Keywords)
                            || t.Community.Location.Contains(options.Keywords)
                            || t.Community.Traffic.Contains(options.Keywords));
                }
                long totalCount = items.LongCount();//总搜索结果条数

                items = items.Include(h => h.Attachments).Include(h => h.Community)
                    .Include(nameof(HouseEntity.Community) + "." + nameof(CommunityEntity.Region)
                        + "." + nameof(RegionEntity.City))
                    .Include(nameof(HouseEntity.Community) + "." + nameof(CommunityEntity.Region))
                    .Include(h => h.DecorateStatus)
                    .Include(h => h.Housepics)
                    .Include(h => h.RoomType)
                    .Include(h => h.Status)
                    .Include(h => h.Type).Include(h => h.Attachments);

                switch (options.OrderByType)
                {
                    case HouseSearchOrderByType.AreaAsc:
                        items = items.OrderBy(t => t.Area);
                        break;
                    case HouseSearchOrderByType.AreaDesc:
                        items = items.OrderByDescending(t => t.Area);
                        break;
                    case HouseSearchOrderByType.CreateDateDesc:
                        items = items.OrderByDescending(t => t.CreateDateTime);
                        break;
                    case HouseSearchOrderByType.MonthRentAsc:
                        items = items.OrderBy(t => t.MonthRent);
                        break;
                    case HouseSearchOrderByType.MonthRentDesc:
                        items = items.OrderByDescending(t => t.MonthRent);
                        break;
                }
                //一定不要items.Where
                //而要items=items.Where();
                //OrderBy要在Skip和Take之前
                //给用户看的页码从1开始，程序中是从0开始
                items = items.Skip((options.CurrentIndex - 1) * options.PageSize)
                    .Take(options.PageSize);
                HouseSearchResult searchResult = new HouseSearchResult();
                searchResult.totalCount = totalCount;
                List<HouseDTO> houses = new List<HouseDTO>();
                foreach (var item in items)
                {
                    houses.Add(ToDTO(item));
                }
                searchResult.result = houses.ToList();
                return searchResult;
            }
        }

        public void Update(HouseDTO house)
        {
            using (WarmHomeContext ctx = new WarmHomeContext())
            {
                BaseService<HouseEntity> bs = new BaseService<HouseEntity>(ctx);
                HouseEntity entity = bs.GetById(house.Id);
                entity.Address = house.Address;
                entity.Area = house.Area;
                //2,3,4
                entity.Attachments.Clear();//先删再加
                var atts = ctx.Attachments.Where(a => a.IsDeleted == false &&
                    house.AttachmentIds.Contains(a.Id));
                foreach (AttachmentEntity att in atts)
                {
                    entity.Attachments.Add(att);
                }
                //3,4,5
                entity.CheckInDateTime = house.CheckInDateTime;
                entity.CommunityId = house.CommunityId;
                entity.DecorateStatusId = house.DecorateStatusId;
                entity.Description = house.Description;
                entity.Direction = house.Direction;
                entity.FloorIndex = house.FloorIndex;
                entity.LookableDateTime = house.LookableDateTime;
                entity.MonthRent = house.MonthRent;
                entity.OwnerName = house.OwnerName;
                entity.OwnerPhoneNum = house.OwnerPhoneNum;
                entity.RoomTypeId = house.RoomTypeId;
                entity.StatusId = house.StatusId;
                entity.TotalFloorCount = house.TotalFloorCount;
                entity.TypeId = house.TypeId;
                ctx.SaveChanges();
            }
        }

        public IEnumerable<HouseDTO> GetAll(long cityId, long typeId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(db);
                var data = service.GetAll().Include(e => e.Community.Region)
                    .Include(e => e.Community)
                    .Include(e => e.Housepics)
                    .Include(e => e.Community.Region.City)
                    .Include(e => e.RoomType)
                    .Include(e => e.Status)
                    .Include(e => e.DecorateStatus)
                    .Include(e => e.Type)
                    .Include(e=>e.Attachments)
                    .AsNoTracking()
                    .Where(e => e.Community.Region.CityId == cityId && e.TypeId == typeId);
                  
                return data.ToList().Select(item => ToDTO(item));
            }
        }
    }
}
