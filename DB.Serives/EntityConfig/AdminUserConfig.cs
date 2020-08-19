using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public class AdminUserConfig:EntityTypeConfiguration<AdminUserEntity>
    {
        public AdminUserConfig()
        {
            ToTable("T_AdminUser");
            //一般配置到“多”端，因为“一端”可能根本不知道“多端”的存在
            HasOptional(u => u.City).WithMany().HasForeignKey(u => u.CityId).WillCascadeOnDelete(false);
            HasMany(e => e.Roles).WithMany(r => r.AdminUsers).Map(e => e.ToTable("AdminUserRoles").MapLeftKey("AdminUserId").MapRightKey("RoleId"));
            Property(e => e.Email).HasMaxLength(50).IsRequired().IsUnicode();
            this.Property(e => e.Name).HasMaxLength(50).IsRequired().IsUnicode();
            this.Property(e => e.PasswordHash).HasMaxLength(250).IsRequired().IsUnicode();
            this.Property(e => e.PasswordSalt).HasMaxLength(10).IsRequired().IsUnicode();
            this.Property(e => e.PhoneNum).IsRequired().HasMaxLength(20).IsUnicode();

        }
    }
}
