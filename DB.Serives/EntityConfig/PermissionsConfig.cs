using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
     public  class PermissionsConfig:EntityTypeConfiguration<PermissionEntity>
    {
        public PermissionsConfig()
        {
            ToTable("T_Permissions");
            this.Property(e => e.Name).HasMaxLength(50).IsRequired();
            this.Property(e => e.Description).IsRequired().HasMaxLength(500);
        }
    }
}
