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
    class HouseAppointmentService : IHouseAppointmentService
    {
        public long AddHouseAppointment(long? userid, string name, string phoneNum, long houseId, DateTime visiteDate)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                HouseAppointmentEntity house = new HouseAppointmentEntity()
                {
                    UserId = userid,
                    Name = name,
                    PhoneNum = phoneNum,
                    HouseId = houseId,
                    Status = "未处理",
                    VisitDate = visiteDate
                          
                };
                db.HouseAppointments.Add(house);
                db.SaveChanges();
                return house.Id;
            }
        }

        public bool Follow(long adminUserId, long houseAppointmentId)
        {
            throw new NotImplementedException();
        }

        public HouseAppointmentDTO GetById(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HouseAppointmentEntity> service = new BaseService<HouseAppointmentEntity>(db);
                var data= service.GetAll().Include(e => e.House.Community.Region).Include(e => e.House.Community).Include(e => e.FollowAdminUser).AsNoTracking().SingleOrDefault(e => e.Id == id);
                if (data==null)
                {
                    return null;
                }
                return DTO(data);
            }
        }
        public HouseAppointmentDTO DTO(HouseAppointmentEntity entity) {
            string adminUserName="";
            if (entity.FollowAdminUser != null)
            {
                adminUserName = entity.FollowAdminUser.Name;
            }
            HouseAppointmentDTO house = new HouseAppointmentDTO()
            {
                Id = entity.Id,
                VisitDate = entity.VisitDate,
                CreateDateTime = entity.CreateDateTime,
                HouseId = entity.HouseId,
                PhoneNum = entity.PhoneNum,
                Name = entity.Name,
                Status = entity.Status,
                UserId = entity.UserId,
                FollowAdminUserId = entity.FollowAdminUserId,
                FollowAdminUserName = adminUserName,
                CommunityName = entity.House.Community.Name,
                RegionName = entity.House.Community.Region.Name    
            };
            return house;
        }

        public IEnumerable<HouseAppointmentDTO> GetPagedData(long cityId, string status, int pageSize, int currentIndex)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HouseAppointmentEntity> service = new BaseService<HouseAppointmentEntity>(db);
               var data=  service.GetAll().Include(e => e.House.Community).Include(e => e.House.Community.Region).Include(e => e.FollowAdminUser).AsNoTracking().Where(e => e.House.Community.Region.CityId == cityId && e.Status == status).OrderBy(e => e.CreateDateTime).Skip(pageSize).Take(currentIndex);
                return data.Select(item => DTO(item)).ToList();
            }
        }

        public long GetTotalCount(long cityId, string status)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HouseAppointmentEntity> service = new BaseService<HouseAppointmentEntity>(db);
                return service.GetAll().Include(e => e.House.Community.Region).AsNoTracking().Where(e => e.Status == status).LongCount();
            }
        }
    }
}
