namespace Instagram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(nullable: false),
                        time = c.DateTime(nullable: false),
                        postId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Posts", t => t.postId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.postId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        time = c.DateTime(nullable: false),
                        active = c.Boolean(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: false)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        username = c.String(nullable: false),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        dob = c.DateTime(nullable: false),
                        accountPrivacy = c.Boolean(nullable: false),
                        profilePic = c.String(),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        user1Id = c.Int(nullable: false),
                        user2Id = c.Int(nullable: false),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => new { t.user1Id, t.user2Id })
                .ForeignKey("dbo.Users", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.PostImages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        postId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Posts", t => t.postId, cascadeDelete: true)
                .Index(t => t.postId);
            
            CreateTable(
                "dbo.PostLikes",
                c => new
                    {
                        userId = c.Int(nullable: false),
                        postId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.userId, t.postId })
                .ForeignKey("dbo.Posts", t => t.postId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId)
                .Index(t => t.postId);
            
            CreateTable(
                "dbo.PostWithImages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        images_id = c.Int(),
                        post_id = c.Int(),
                        postImages_id = c.Int(),
                        postRef_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.PostImages", t => t.images_id)
                .ForeignKey("dbo.Posts", t => t.post_id)
                .ForeignKey("dbo.PostImages", t => t.postImages_id)
                .ForeignKey("dbo.Posts", t => t.postRef_id)
                .Index(t => t.images_id)
                .Index(t => t.post_id)
                .Index(t => t.postImages_id)
                .Index(t => t.postRef_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostWithImages", "postRef_id", "dbo.Posts");
            DropForeignKey("dbo.PostWithImages", "postImages_id", "dbo.PostImages");
            DropForeignKey("dbo.PostWithImages", "post_id", "dbo.Posts");
            DropForeignKey("dbo.PostWithImages", "images_id", "dbo.PostImages");
            DropForeignKey("dbo.PostLikes", "userId", "dbo.Users");
            DropForeignKey("dbo.PostLikes", "postId", "dbo.Posts");
            DropForeignKey("dbo.PostImages", "postId", "dbo.Posts");
            DropForeignKey("dbo.Followers", "user_id", "dbo.Users");
            DropForeignKey("dbo.Comments", "userId", "dbo.Users");
            DropForeignKey("dbo.Comments", "postId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "userId", "dbo.Users");
            DropIndex("dbo.PostWithImages", new[] { "postRef_id" });
            DropIndex("dbo.PostWithImages", new[] { "postImages_id" });
            DropIndex("dbo.PostWithImages", new[] { "post_id" });
            DropIndex("dbo.PostWithImages", new[] { "images_id" });
            DropIndex("dbo.PostLikes", new[] { "postId" });
            DropIndex("dbo.PostLikes", new[] { "userId" });
            DropIndex("dbo.PostImages", new[] { "postId" });
            DropIndex("dbo.Followers", new[] { "user_id" });
            DropIndex("dbo.Posts", new[] { "userId" });
            DropIndex("dbo.Comments", new[] { "userId" });
            DropIndex("dbo.Comments", new[] { "postId" });
            DropTable("dbo.PostWithImages");
            DropTable("dbo.PostLikes");
            DropTable("dbo.PostImages");
            DropTable("dbo.Followers");
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
