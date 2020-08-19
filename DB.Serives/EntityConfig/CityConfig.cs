using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public class CityConfig:EntityTypeConfiguration<CityEntity>
    {
        public CityConfig()
        {
            ToTable("T_Citys");
            this.Property(e => e.Name).HasMaxLength(250).IsRequired();   
        }

    }
}
