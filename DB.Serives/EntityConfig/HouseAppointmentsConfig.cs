using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
   public  class HouseAppointmentsConfig:EntityTypeConfiguration<HouseAppointmentEntity>
    {
        public HouseAppointmentsConfig()
        {
            ToTable("T_HouseAppointments");
            this.Property(e => e.PhoneNum).HasMaxLength(20).IsRequired();
            this.Property(e => e.Name).IsRequired().HasMaxLength(100);
            this.Property(e => e.Status).HasMaxLength(100).IsRequired();
            HasRequired(e => e.House).WithMany().HasForeignKey(e => e.HouseId).WillCascadeOnDelete(false);
            HasRequired(e => e.User).WithMany().HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            HasRequired(e => e.FollowAdminUser).WithMany().HasForeignKey(e => e.FollowAdminUserId).WillCascadeOnDelete(false);
        }
    }
}
