namespace Instagram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPostWithImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostWithImages", "images_id", "dbo.PostImages");
            DropForeignKey("dbo.PostWithImages", "post_id", "dbo.Posts");
            DropForeignKey("dbo.PostWithImages", "postImages_id", "dbo.PostImages");
            DropForeignKey("dbo.PostWithImages", "postRef_id", "dbo.Posts");
            DropIndex("dbo.PostWithImages", new[] { "images_id" });
            DropIndex("dbo.PostWithImages", new[] { "post_id" });
            DropIndex("dbo.PostWithImages", new[] { "postImages_id" });
            DropIndex("dbo.PostWithImages", new[] { "postRef_id" });
            DropTable("dbo.PostWithImages");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.PostWithImages", "postRef_id");
            CreateIndex("dbo.PostWithImages", "postImages_id");
            CreateIndex("dbo.PostWithImages", "post_id");
            CreateIndex("dbo.PostWithImages", "images_id");
            AddForeignKey("dbo.PostWithImages", "postRef_id", "dbo.Posts", "id");
            AddForeignKey("dbo.PostWithImages", "postImages_id", "dbo.PostImages", "id");
            AddForeignKey("dbo.PostWithImages", "post_id", "dbo.Posts", "id");
            AddForeignKey("dbo.PostWithImages", "images_id", "dbo.PostImages", "id");
        }
    }
}
