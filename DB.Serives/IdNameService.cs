using DB.IService;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.Service
{
    class IdNameService : IIdNameService
    {
        public long AddIdName(string typeName, string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<IdNameEntity> serives = new BaseService<IdNameEntity>(db);
                bool esixts= serives.GetAll().Any(e => e.TypeName == typeName && e.Name == name);
                if (esixts)
                {
                    throw new ArgumentException("此数据已存在");
                }
                else
                {
                    IdNameEntity entity = new IdNameEntity()
                    {
                         Name=name,
                          TypeName=typeName
                    };
                    db.IdNames.Add(entity);
                    db.SaveChanges();
                    return entity.Id;
                }
                
            }
        }

        public IEnumerable<IdNameDTO> GetAll(string typeName)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<IdNameEntity> serives = new BaseService<IdNameEntity>(db);
                return serives.GetAll().Where(e=>e.TypeName==typeName).ToList(). Select(item => new IdNameDTO()
                                                                                        {
                                                                                            Id = item.Id,
                                                                                            Name = item.Name,
                                                                                            TypeName = item.TypeName,
                                                                                            CreateDateTime = item.CreateDateTime
                                                                                        });
            }
        }

        public IdNameDTO GetById(long id)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {

                BaseService<IdNameEntity> serives = new BaseService<IdNameEntity>(db);
                var data = serives.GetById(id);
                if (data==null)
                {
                    throw new ArgumentException("没有此数据");
                }
                else
                {
                    IdNameDTO idName = new IdNameDTO()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        TypeName = data.TypeName,
                        CreateDateTime = data.CreateDateTime
                    };
                    return idName;
                }
            }
        }
    }
}
