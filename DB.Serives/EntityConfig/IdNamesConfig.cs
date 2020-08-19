using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
   public  class IdNamesConfig:EntityTypeConfiguration<IdNameEntity>
    {
        public IdNamesConfig()
        {
            ToTable("IdNames");
            this.Property(e => e.TypeName).HasMaxLength(50).IsRequired();
            this.Property(e => e.Name).HasMaxLength(200).IsRequired();
        }
    }
}
