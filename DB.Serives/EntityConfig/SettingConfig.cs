using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public class SettingConfig:EntityTypeConfiguration<SettingEntity>
    {
        public SettingConfig()
        {
            ToTable("T_Settings");
            //配置项名称
            this.Property(e => e.Name).HasMaxLength(250).IsRequired();
            //配置项的值  用于存取短信api接口
            this.Property(e => e.Value).IsRequired();
        }
    }
}
