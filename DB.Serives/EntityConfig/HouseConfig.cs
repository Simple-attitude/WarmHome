using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public class HouseConfig:EntityTypeConfiguration<HouseEntity>
    {
        public HouseConfig()
        {
            ToTable("T_Houses");
            HasRequired(e => e.Community).WithMany().HasForeignKey(e => e.CommunityId).WillCascadeOnDelete(false);
            HasRequired(e => e.Type).WithMany().HasForeignKey(e => e.TypeId).WillCascadeOnDelete(false);
            HasRequired(e => e.DecorateStatus).WithMany().HasForeignKey(e => e.DecorateStatusId).WillCascadeOnDelete(false);
            HasRequired(e => e.Status).WithMany().HasForeignKey(e => e.StatusId).WillCascadeOnDelete(false);
            HasRequired(e => e.RoomType).WithMany().HasForeignKey(e => e.RoomTypeId).WillCascadeOnDelete(false);
            this.Property(e => e.Address).HasMaxLength(50).IsRequired();
            this.Property(e => e.Description).HasMaxLength(50).IsRequired();
            Property(h => h.Description).IsOptional();
            Property(h => h.OwnerName).IsRequired().HasMaxLength(20);
            Property(h => h.OwnerPhoneNum).IsRequired().HasMaxLength(20).IsUnicode(false);

        }
    }
}
