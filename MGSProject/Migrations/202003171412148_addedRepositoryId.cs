namespace MGSProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRepositoryId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Repositories");
            DropColumn("dbo.Repositories", "Id");
            AddColumn("dbo.Repositories", "RepositoryId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Repositories", "RepositoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Repositories", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Repositories");
            DropColumn("dbo.Repositories", "RepositoryId");
            AddPrimaryKey("dbo.Repositories", "Id");
        }
    }
}
