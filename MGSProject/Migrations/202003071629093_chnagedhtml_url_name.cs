namespace MGSProject.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class chnagedhtml_url_name : DbMigration 
    {
        public override void Up()
        {
            AddColumn("dbo.Repositories", "HtmlUrl", c => c.String());
            DropColumn("dbo.Repositories", "html_url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Repositories", "html_url", c => c.String());
            DropColumn("dbo.Repositories", "HtmlUrl");
        }
    }
}
