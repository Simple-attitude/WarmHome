namespace DB.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCommuntityName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.T_Communitie", newName: "T_Community");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.T_Community", newName: "T_Communitie");
        }
    }
}
