namespace DB.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Community_builtYearNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Community", "BuiltYear", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Community", "BuiltYear", c => c.Int(nullable: false));
        }
    }
}
