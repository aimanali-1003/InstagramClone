namespace Instagram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBUpdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false));
        }
    }
}
