using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
    public  class AttachmentConfig:EntityTypeConfiguration<AttachmentEntity>
    {
        public AttachmentConfig()
        {
            ToTable("T_Attachments");
            this.Property(e => e.Name).HasMaxLength(100).IsRequired();
            this.Property(e => e.IconName).IsRequired().HasMaxLength(100);
            HasMany(e => e.Houses).WithMany(e => e.Attachments).Map(e => e.ToTable("T_HouseAttachments").MapLeftKey("HouseId").MapRightKey("AttachmentsId"));

        }
    }
}
