namespace CMDLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedUsersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "Rented", c => c.Boolean(nullable: false));
            AddColumn("dbo.Books", "User_Id", c => c.Int());
            CreateIndex("dbo.Books", "User_Id");
            AddForeignKey("dbo.Books", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "User_Id", "dbo.Users");
            DropIndex("dbo.Books", new[] { "User_Id" });
            DropColumn("dbo.Books", "User_Id");
            DropColumn("dbo.Books", "Rented");
            DropTable("dbo.Users");
        }
    }
}
