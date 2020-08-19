namespace DB.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_AdminLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        Msg = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PhoneNum = c.String(nullable: false, maxLength: 20),
                        PasswordHash = c.String(nullable: false, maxLength: 100),
                        PasswordSalt = c.String(nullable: false, maxLength: 20),
                        LoginErrorTime = c.Int(nullable: false),
                        LastLoginErrorTime = c.DateTime(),
                        CityId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Citys", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.T_Citys",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_AdminUser",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        PhoneNum = c.String(nullable: false, maxLength: 20),
                        PasswordSalt = c.String(nullable: false, maxLength: 10),
                        PasswordHash = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 50),
                        CityId = c.Long(),
                        LoginErrorTimes = c.Int(nullable: false),
                        LastLoginErrorDateTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Citys", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.T_Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_Permissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_Attachments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        IconName = c.String(nullable: false, maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_Houses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CommunityId = c.Long(nullable: false),
                        RoomTypeId = c.Long(nullable: false),
                        Address = c.String(nullable: false, maxLength: 50),
                        MonthRent = c.Int(nullable: false),
                        StatusId = c.Long(nullable: false),
                        Area = c.Double(nullable: false),
                        DecorateStatusId = c.Long(nullable: false),
                        FloorIndex = c.Int(nullable: false),
                        TypeId = c.Long(nullable: false),
                        Direction = c.String(),
                        LookableDateTime = c.DateTime(nullable: false),
                        CheckinDateTime = c.DateTime(nullable: false),
                        OwnerName = c.String(nullable: false, maxLength: 20),
                        OwnerPhoneNum = c.String(nullable: false, maxLength: 20, unicode: false),
                        Description = c.String(maxLength: 50),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Communitie", t => t.CommunityId)
                .ForeignKey("dbo.IdNames", t => t.DecorateStatusId)
                .ForeignKey("dbo.IdNames", t => t.RoomTypeId)
                .ForeignKey("dbo.IdNames", t => t.StatusId)
                .ForeignKey("dbo.IdNames", t => t.TypeId)
                .Index(t => t.CommunityId)
                .Index(t => t.RoomTypeId)
                .Index(t => t.StatusId)
                .Index(t => t.DecorateStatusId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.T_Communitie",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        RegionId = c.Long(nullable: false),
                        Location = c.String(nullable: false, maxLength: 100),
                        Traffic = c.String(),
                        BuildYear = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Regions", t => t.RegionId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.T_Regions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        CityId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Citys", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.IdNames",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_HousePics",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HouseId = c.Long(nullable: false),
                        Url = c.String(nullable: false, maxLength: 250),
                        ThumbUrl = c.String(nullable: false, maxLength: 250),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        HouseEntity_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_Houses", t => t.HouseId)
                .ForeignKey("dbo.T_Houses", t => t.HouseEntity_Id)
                .Index(t => t.HouseId)
                .Index(t => t.HouseEntity_Id);
            
            CreateTable(
                "dbo.T_HouseAppointments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        PhoneNum = c.String(nullable: false, maxLength: 20),
                        VisitDate = c.DateTime(nullable: false),
                        HouseId = c.Long(nullable: false),
                        Status = c.String(nullable: false, maxLength: 100),
                        FollowAdminUserId = c.Long(nullable: false),
                        FollowDateTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.T_AdminUser", t => t.FollowAdminUserId)
                .ForeignKey("dbo.T_Houses", t => t.HouseId)
                .ForeignKey("dbo.T_Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HouseId)
                .Index(t => t.FollowAdminUserId);
            
            CreateTable(
                "dbo.T_Settings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Value = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        RoleId = c.Long(nullable: false),
                        PermissionsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermissionsId })
                .ForeignKey("dbo.T_Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.T_Permissions", t => t.PermissionsId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionsId);
            
            CreateTable(
                "dbo.AdminUserRoles",
                c => new
                    {
                        AdminUserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AdminUserId, t.RoleId })
                .ForeignKey("dbo.T_AdminUser", t => t.AdminUserId, cascadeDelete: true)
                .ForeignKey("dbo.T_Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.AdminUserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.T_HouseAttachments",
                c => new
                    {
                        HouseId = c.Long(nullable: false),
                        AttachmentsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.HouseId, t.AttachmentsId })
                .ForeignKey("dbo.T_Attachments", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("dbo.T_Houses", t => t.AttachmentsId, cascadeDelete: true)
                .Index(t => t.HouseId)
                .Index(t => t.AttachmentsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_HouseAppointments", "UserId", "dbo.T_Users");
            DropForeignKey("dbo.T_HouseAppointments", "HouseId", "dbo.T_Houses");
            DropForeignKey("dbo.T_HouseAppointments", "FollowAdminUserId", "dbo.T_AdminUser");
            DropForeignKey("dbo.T_HouseAttachments", "AttachmentsId", "dbo.T_Houses");
            DropForeignKey("dbo.T_HouseAttachments", "HouseId", "dbo.T_Attachments");
            DropForeignKey("dbo.T_Houses", "TypeId", "dbo.IdNames");
            DropForeignKey("dbo.T_Houses", "StatusId", "dbo.IdNames");
            DropForeignKey("dbo.T_Houses", "RoomTypeId", "dbo.IdNames");
            DropForeignKey("dbo.T_HousePics", "HouseEntity_Id", "dbo.T_Houses");
            DropForeignKey("dbo.T_HousePics", "HouseId", "dbo.T_Houses");
            DropForeignKey("dbo.T_Houses", "DecorateStatusId", "dbo.IdNames");
            DropForeignKey("dbo.T_Houses", "CommunityId", "dbo.T_Communitie");
            DropForeignKey("dbo.T_Communitie", "RegionId", "dbo.T_Regions");
            DropForeignKey("dbo.T_Regions", "CityId", "dbo.T_Citys");
            DropForeignKey("dbo.AdminUserRoles", "RoleId", "dbo.T_Roles");
            DropForeignKey("dbo.AdminUserRoles", "AdminUserId", "dbo.T_AdminUser");
            DropForeignKey("dbo.RolePermissions", "PermissionsId", "dbo.T_Permissions");
            DropForeignKey("dbo.RolePermissions", "RoleId", "dbo.T_Roles");
            DropForeignKey("dbo.T_AdminUser", "CityId", "dbo.T_Citys");
            DropForeignKey("dbo.T_AdminLogs", "UserId", "dbo.T_Users");
            DropForeignKey("dbo.T_Users", "CityId", "dbo.T_Citys");
            DropIndex("dbo.T_HouseAttachments", new[] { "AttachmentsId" });
            DropIndex("dbo.T_HouseAttachments", new[] { "HouseId" });
            DropIndex("dbo.AdminUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AdminUserRoles", new[] { "AdminUserId" });
            DropIndex("dbo.RolePermissions", new[] { "PermissionsId" });
            DropIndex("dbo.RolePermissions", new[] { "RoleId" });
            DropIndex("dbo.T_HouseAppointments", new[] { "FollowAdminUserId" });
            DropIndex("dbo.T_HouseAppointments", new[] { "HouseId" });
            DropIndex("dbo.T_HouseAppointments", new[] { "UserId" });
            DropIndex("dbo.T_HousePics", new[] { "HouseEntity_Id" });
            DropIndex("dbo.T_HousePics", new[] { "HouseId" });
            DropIndex("dbo.T_Regions", new[] { "CityId" });
            DropIndex("dbo.T_Communitie", new[] { "RegionId" });
            DropIndex("dbo.T_Houses", new[] { "TypeId" });
            DropIndex("dbo.T_Houses", new[] { "DecorateStatusId" });
            DropIndex("dbo.T_Houses", new[] { "StatusId" });
            DropIndex("dbo.T_Houses", new[] { "RoomTypeId" });
            DropIndex("dbo.T_Houses", new[] { "CommunityId" });
            DropIndex("dbo.T_AdminUser", new[] { "CityId" });
            DropIndex("dbo.T_Users", new[] { "CityId" });
            DropIndex("dbo.T_AdminLogs", new[] { "UserId" });
            DropTable("dbo.T_HouseAttachments");
            DropTable("dbo.AdminUserRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.T_Settings");
            DropTable("dbo.T_HouseAppointments");
            DropTable("dbo.T_HousePics");
            DropTable("dbo.IdNames");
            DropTable("dbo.T_Regions");
            DropTable("dbo.T_Communitie");
            DropTable("dbo.T_Houses");
            DropTable("dbo.T_Attachments");
            DropTable("dbo.T_Permissions");
            DropTable("dbo.T_Roles");
            DropTable("dbo.T_AdminUser");
            DropTable("dbo.T_Citys");
            DropTable("dbo.T_Users");
            DropTable("dbo.T_AdminLogs");
        }
    }
}
