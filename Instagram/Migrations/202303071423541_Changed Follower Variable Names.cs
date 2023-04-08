namespace Instagram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedFollowerVariableNames : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Followers");
            AddColumn("dbo.Followers", "FollowedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Followers", "FollowedUser", c => c.Int(nullable: false));
            DropColumn("dbo.Followers", "user1Id");
            DropColumn("dbo.Followers", "user2Id");
            AddPrimaryKey("dbo.Followers", new[] { "FollowedBy", "FollowedUser" });
        }
        
        public override void Down()
        {
            AddColumn("dbo.Followers", "user2Id", c => c.Int(nullable: false));
            AddColumn("dbo.Followers", "user1Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Followers");
            DropColumn("dbo.Followers", "FollowedUser");
            DropColumn("dbo.Followers", "FollowedBy");
            AddPrimaryKey("dbo.Followers", new[] { "user1Id", "user2Id" });
        }
    }
}
