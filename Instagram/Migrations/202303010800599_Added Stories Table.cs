namespace Instagram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStoriesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        storyImage = c.String(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stories", "userId", "dbo.Users");
            DropIndex("dbo.Stories", new[] { "userId" });
            DropTable("dbo.Stories");
        }
    }
}
