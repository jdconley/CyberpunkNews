namespace CyberpunkNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeTopicTitleAndUrlRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.topics", "title", c => c.String(nullable: false));
            AlterColumn("dbo.topics", "url", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.topics", "url", c => c.String());
            AlterColumn("dbo.topics", "title", c => c.String());
        }
    }
}
