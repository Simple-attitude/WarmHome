using DB.Service.Entitys;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service
{
    public class WarmHomeContext:DbContext
    {
        private static ILog Log = LogManager.GetLogger(typeof(WarmHomeContext));
        public WarmHomeContext():base("name=WarmHome")
        {
            //this.Database.Log = (sql) =>
            //{
            //    Log.DebugFormat("执行Sql语句：{0}", sql);
            //};
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<AdminLogEntity> AdminLogs { set; get; }
        public DbSet<AdminUserEntity>  AdminUsers { get; set; }

        public DbSet<AttachmentEntity>  Attachments { get; set; }
        public DbSet<CityEntity>  Cities { get; set; }
        public DbSet<CommunityEntity>  Communities { get; set; }
        public DbSet<HouseAppointmentEntity>  HouseAppointments { get; set; }
        public DbSet<HouseEntity>  Houses { get; set; }
        public DbSet<HousepicEntity>  Housepics { get; set; }
        public DbSet<IdNameEntity>  IdNames { get; set; }
        public DbSet<PermissionEntity>  Permissions { get; set; }
        public DbSet<RegionEntity>  Regions { get; set; }
        public DbSet<RolesEntity>  Roles { get; set; }
        public DbSet<SettingEntity>  Settings { get; set; }
        public DbSet<UserEntity>  Users { get; set; }
    }
}
