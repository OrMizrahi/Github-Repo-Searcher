﻿namespace MGSProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedagain : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Repositories");
            AlterColumn("dbo.Repositories", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Repositories", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Repositories");
            AlterColumn("dbo.Repositories", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Repositories", "Id");
        }
    }
}
