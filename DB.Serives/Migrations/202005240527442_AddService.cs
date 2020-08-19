namespace DB.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Houses", "TotalFloorCount", c => c.Int(nullable: false));
            AddColumn("dbo.T_Communitie", "BuiltYear", c => c.Int(nullable: false));
            AlterColumn("dbo.T_Houses", "Area", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.T_Communitie", "BuildYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_Communitie", "BuildYear", c => c.Int(nullable: false));
            AlterColumn("dbo.T_Houses", "Area", c => c.Double(nullable: false));
            DropColumn("dbo.T_Communitie", "BuiltYear");
            DropColumn("dbo.T_Houses", "TotalFloorCount");
        }
    }
}
