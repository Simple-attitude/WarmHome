using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public class UsersConfig:EntityTypeConfiguration<UserEntity>
    {
        public UsersConfig()
        {
            ToTable("T_Users");
            this.Property(e => e.PasswordSalt).HasMaxLength(20).IsRequired();
            this.Property(e => e.PasswordHash).HasMaxLength(100).IsRequired();
            this.Property(e => e.PhoneNum).IsRequired().HasMaxLength(20);
            HasRequired(e => e.City).WithMany().HasForeignKey(e => e.CityId).WillCascadeOnDelete(false);
        }
    }
}
