using DB.IService;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.Service
{
    class SettingsService : ISettingsService
    {

        /// <summary>
        /// 获取bool值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ? GetBoolValue(string name)
        {
            if (GetValue(name)==null)
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(GetValue(name));
            }
        }
        /// <summary>
        /// 获取Settings表的整数值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int? GetIntValue(string name)
        {
            if (GetValue(name)==null)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(GetValue(name));
            }
        }
        /// <summary>
        /// 获取Settings表的所有数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<SettingsDTO> GetSettings()
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<SettingEntity> serives = new BaseService<SettingEntity>(db);
                return  serives.GetAll().Select(item => new SettingsDTO()
                                                                        {
                                                                            Id = item.Id,
                                                                            Name = item.Name,
                                                                            Value = item.Value,
                                                                            CreateDateTime = item.CreateDateTime
                                                                        }).ToList();
            }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="name">要查询的名称</param>
        /// <returns></returns>
        public string GetValue(string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<SettingEntity> serives = new BaseService<SettingEntity>(db);
                var data= serives.GetAll().AsNoTracking().SingleOrDefault(e => e.Name == name);
                if (data==null)
                {
                    return null;
                }
                else
                {
                    return data.Value;
                }
            }
        }

        public void SetBoolValue(string name, string value)
        {
            SetValue(name, value.ToString());
        }

        public void SetIntValue(string name, string value)
        {
            SetValue(name, value.ToString());
        }

        public void SetValue(string name, string value)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<SettingEntity> serives = new BaseService<SettingEntity>(db);
                var data = serives.GetAll().SingleOrDefault(e => e.Name == name);
                if (data == null)
                {
                    SettingEntity setting = new SettingEntity()
                    {
                        Name = name,
                        Value = value
                    };
                    db.Settings.Add(setting);
                }
                else
                {
                    data.Value = value;
                }
                db.SaveChanges();
            }
        }
    }
}
