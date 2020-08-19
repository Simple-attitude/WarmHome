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
    class RegionService : IRegionService
    {
        public RegionDTO GetById(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RegionEntity> service = new BaseService<RegionEntity>(db);
                var data= service.GetAll().Include(e => e.City).SingleOrDefault(e => e.Id == id);
                if (data==null)
                {
                    return null;
                }
                else
                {
                    return GetDTO(data);
                }
            }
        }
        public RegionDTO GetDTO(RegionEntity entity)
        {
            RegionDTO region = new RegionDTO()
            {
                CityId = entity.CityId,
                CityName = entity.City.Name,
                Name = entity.Name,
                CreateDateTime = entity.CreateDateTime,
                Id = entity.Id
            };
            return region;
        }

        public IEnumerable<RegionDTO> GetCityAll(long cityId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RegionEntity> service = new BaseService<RegionEntity>(db);
                return service.GetAll().Include(e=>e.City).Where(e => e.CityId == cityId).ToList().Select(item => GetDTO(item));
            }
        }
        public IEnumerable<RegionDTO> GetAll()
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RegionEntity> service = new BaseService<RegionEntity>(db);
                return service.GetAll().Include(e => e.City).ToList().Select(item => GetDTO(item));
            }
        }

        public void AddRegion(long cityId, string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RegionEntity> service = new BaseService<RegionEntity>(db);
                bool exists= service.GetAll().Any(e => e.Name == name);
                if (exists)
                {
                    throw new ArgumentException("此" + name + "小区已存在");
                }
                RegionEntity entity = new RegionEntity()
                {
                     Name=name,
                     CityId=cityId                       
                };
                db.Regions.Add(entity);
                db.SaveChanges();
            }
        }

        public void UpdateRegion(long id,long cityId, string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RegionEntity> service = new BaseService<RegionEntity>(db);
                var data= service.GetById(id);
                if (data==null)
                {
                    throw new ArgumentException("此" + name + "区域不存在");
                }
                data.CityId = cityId;
                data.Name = name;
                db.SaveChanges();
            }
        }

        public void Deleted(long id)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<RegionEntity> service = new BaseService<RegionEntity>(db);
                service.MarkDeleted(id);
                
            }
        }
    }
}
