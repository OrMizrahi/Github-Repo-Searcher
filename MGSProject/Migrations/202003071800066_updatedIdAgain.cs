namespace MGSProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedIdAgain : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Repositories");
            AlterColumn("dbo.Repositories", "Id", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.Repositories", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Repositories");
            AlterColumn("dbo.Repositories", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Repositories", "Id");
        }
    }
}
