using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public class CommunityConfig:EntityTypeConfiguration<CommunityEntity>
    {
        public CommunityConfig()
        {
            ToTable("T_Community");
            this.Property(e => e.Name).HasMaxLength(100).IsRequired();
            this.Property(e => e.Location).HasMaxLength(100).IsRequired();
            this.Property(e => e.Location).HasMaxLength(100).IsRequired();
            HasRequired(e => e.Region).WithMany().HasForeignKey(e => e.RegionId).WillCascadeOnDelete(false);
        }
    }
}
