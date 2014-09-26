namespace CyberpunkNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicKarma : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.topics", "karma", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.topics", "karma");
        }
    }
}
