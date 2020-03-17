namespace MGSProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedRepositoryAgain2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Repositories");
            AlterColumn("dbo.Repositories", "RepositoryId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Repositories", "RepositoryId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Repositories");
            AlterColumn("dbo.Repositories", "RepositoryId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Repositories", "RepositoryId");
        }
    }
}
