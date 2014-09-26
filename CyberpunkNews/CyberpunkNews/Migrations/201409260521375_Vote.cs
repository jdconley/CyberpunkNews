namespace CyberpunkNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.votes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        topic_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.topics", t => t.topic_id)
                .Index(t => t.topic_id);
            
            AddColumn("dbo.topics", "submitDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropColumn("dbo.topics", "submit_date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.topics", "submit_date", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropForeignKey("dbo.votes", "topic_id", "dbo.topics");
            DropIndex("dbo.votes", new[] { "topic_id" });
            DropColumn("dbo.topics", "submitDate");
            DropTable("dbo.votes");
        }
    }
}
