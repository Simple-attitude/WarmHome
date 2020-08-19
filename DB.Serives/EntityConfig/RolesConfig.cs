using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public class RolesConfig:EntityTypeConfiguration<RolesEntity>
    {
        public RolesConfig()
        {
            ToTable("T_Roles");
            this.Property(e => e.Name).HasMaxLength(50).IsRequired();
            HasMany(e => e.Permissions).WithMany(s => s.Roles).Map(e => e.ToTable("RolePermissions").MapLeftKey("RoleId").MapRightKey("PermissionsId"));
        }
    }
}
