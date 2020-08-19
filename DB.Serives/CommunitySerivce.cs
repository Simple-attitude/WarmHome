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
    class CommunitySerivce : ICommunityService
    {
        public IEnumerable<CommunityDTO> GetByRegionId(long regionId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CommunityEntity> service = new BaseService<CommunityEntity>(db);
                var data= service.GetAll().Include(e=>e.Region).AsNoTracking().Where(e => e.RegionId == regionId);
                if (data.Count()>0)
                {
                    return data.ToList().Select(item => DTO(item));
                }
                else
                {
                    return null;
                }
            }
        }
        public CommunityDTO DTO(CommunityEntity entity) {
            CommunityDTO community = new CommunityDTO()
            {
                BuiltYear = entity.BuiltYear,
                Id = entity.Id,
                CreateDateTime = entity.CreateDateTime,
                Location = entity.Location,
                Name = entity.Name,
                RegionId = entity.RegionId,
                Traffic = entity.Traffic,
                RegionName = entity.Region.Name
              
            };
            return community;
        }

        public IEnumerable<CommunityDTO> GetAll()
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<CommunityEntity> service = new BaseService<CommunityEntity>(db);
                var data = service.GetAll().Include(e => e.Region).AsNoTracking();
                if (data.Count() > 0)
                {
                    return data.ToList().Select(item => DTO(item));
                }
                else
                {
                    return null;
                }
            }
        }

        public void AddCommunity(CommunityDTO community)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<CommunityEntity> service = new BaseService<CommunityEntity>(db);
                CommunityEntity entity = new CommunityEntity()
                {
                    RegionId = community.RegionId,
                    Name = community.Name,
                    Location=community.Location,
                    BuiltYear=community.BuiltYear,
                    Traffic=community.Traffic    
                };
                db.Communities.Add(entity);
                db.SaveChanges();
            }
        }

        public void UpdateCommunity(CommunityDTO community)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CommunityEntity> service = new BaseService<CommunityEntity>(db);
                var data= service.GetById(community.Id);
                if (data==null)
                {
                    throw new ArgumentException("此" + community.Id + "的小区不存在");
                }
                data.Location = community.Location;
                data.Name = community.Name;
                data.RegionId = community.RegionId;
                data.Traffic = community.Traffic;
                data.BuiltYear = community.BuiltYear;
                db.SaveChanges();
            }
        }

        public void Deleted(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CommunityEntity> service = new BaseService<CommunityEntity>(db);
                service.MarkDeleted(id);
            }
        }


        public CommunityDTO GetById(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<CommunityEntity> service = new BaseService<CommunityEntity>(db);
                var data= service.GetById(id);
                if (data==null)
                {
                    return null;
                }
                return DTO(data);
            }
        }
    }
}
