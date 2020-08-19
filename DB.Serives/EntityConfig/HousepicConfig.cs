using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
   public  class HousepicConfig:EntityTypeConfiguration<HousepicEntity>
    {
        public HousepicConfig()
        {
            ToTable("T_HousePics");
            Property(e => e.Url).HasMaxLength(250).IsRequired();
            Property(e => e.ThumbUrl).HasMaxLength(250).IsRequired();
            HasRequired(e => e.House).WithMany().HasForeignKey(e => e.HouseId).WillCascadeOnDelete(false);
        }
    }
}
