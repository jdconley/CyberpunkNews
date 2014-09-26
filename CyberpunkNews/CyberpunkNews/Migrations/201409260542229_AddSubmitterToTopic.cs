namespace CyberpunkNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubmitterToTopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.topics", "submitter", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.topics", "submitter");
        }
    }
}
