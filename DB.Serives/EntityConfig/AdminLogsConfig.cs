using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.EntityConfig
{
   public  class AdminLogsConfig:EntityTypeConfiguration<AdminLogEntity>
    {
        public AdminLogsConfig()
        {
            ToTable("T_AdminLogs");
            this.Property(e => e.Msg).IsRequired();
            HasRequired(e => e.User).WithMany().HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
        }
    }
}
