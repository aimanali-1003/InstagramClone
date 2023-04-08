namespace Instagram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPostImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostImages", "imageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostImages", "imageUrl");
        }
    }
}
