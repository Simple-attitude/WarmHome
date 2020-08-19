using DB.IService;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.Service
{
    class CityService : ICityService
    {
        public long AddCity(string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CityEntity> serives = new BaseService<CityEntity>(db);
                var result = serives.GetAll().Any(e => e.Name == name);
                if (result)
                {
                    throw new ArgumentException("参数已存在");
                }
                else
                {
                    CityEntity city = new CityEntity()
                    {
                        Name = name
                    };
                    db.Cities.Add(city);
                    db.SaveChanges();
                    return city.Id;
                }
            }
        }

        public IEnumerable<CityDTO> GetAll()
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(db);
                return service.GetAll().AsNoTracking().ToList().Select(item => DTO(item));
            }
        }
        public CityDTO DTO(CityEntity entity) {
            CityDTO city = new CityDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                CreateDateTime = entity.CreateDateTime
            };
            return city;
        }
        public CityDTO GetById(long cityId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(db);
                var data= service.GetById(cityId);
                if (data==null)
                {
                    return null;
                }
                return DTO(data);
            }
        }
        public void Edit(long id,string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(db);
                var data= service.GetById(id);
                if (data==null)
                {
                    throw new ArgumentException("此城市" + id + "不存在");
                }
                data.Name = name;
                db.SaveChanges();
            }
        }
        public void Deleted(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(db);
                service.MarkDeleted(id);
            }
        }
    }
}
