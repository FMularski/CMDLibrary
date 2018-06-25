namespace CMDLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluentAPI_User_Book : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Login", c => c.String());
        }
    }
}
