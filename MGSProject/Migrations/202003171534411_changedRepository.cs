namespace MGSProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedRepository : DbMigration
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
            AddColumn("dbo.Repositories", "Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Repositories");
            DropColumn("dbo.Repositories", "RepositoryId");
            AddPrimaryKey("dbo.Repositories", "Id");
        }
    }
}
