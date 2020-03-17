namespace MGSProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedRepositoryAgain5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Repositories");
            DropColumn("dbo.Repositories", "RepositoryId");
            AddColumn("dbo.Repositories", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Repositories", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Repositories", "RepositoryId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Repositories");
            DropColumn("dbo.Repositories", "Id");
            AddPrimaryKey("dbo.Repositories", "RepositoryId");
        }
    }
}
