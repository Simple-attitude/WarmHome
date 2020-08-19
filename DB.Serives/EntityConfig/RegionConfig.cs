using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public class RegionConfig:EntityTypeConfiguration<RegionEntity>
    {
        public RegionConfig()
        {
            ToTable("T_Regions");
            this.Property(e => e.Name).HasMaxLength(250).IsRequired();
            HasRequired(e => e.City).WithMany().HasForeignKey(e => e.CityId).WillCascadeOnDelete(false);
        }
    }
}
