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
    class AttachmentService : IAttachmentService
    {
        public IEnumerable<AttachmentDTO> GetAll()
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<AttachmentEntity> serives = new BaseService<AttachmentEntity>(db);
                return serives.GetAll().AsNoTracking().ToList().Select(item => DTO(item));
            }
        }
        public AttachmentDTO DTO(AttachmentEntity entity) {
            AttachmentDTO attachment = new AttachmentDTO() {
                Id = entity.Id,
                IconName = entity.IconName,
                Name = entity.Name,
                CreateDateTime = entity.CreateDateTime
            };
            return attachment;

        }

        public IEnumerable<AttachmentDTO> GetHouseId(long houseID)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<HouseEntity> serives = new BaseService<HouseEntity>(db);
                var data = serives.GetAll().Include(e => e.Attachments).AsNoTracking().SingleOrDefault(e => e.Id == houseID);
                if (data==null)
                {
                    throw new ArgumentException("找不到" + houseID + "的数据");
                }
                else
                {
                    return data.Attachments.Select(item => DTO(item));
                }
            }
        }
    }
}
