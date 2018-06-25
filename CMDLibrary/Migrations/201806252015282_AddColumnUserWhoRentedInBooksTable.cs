    namespace CMDLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnUserWhoRentedInBooksTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Books", name: "User_Id", newName: "UserWhoRentedId");
            RenameIndex(table: "dbo.Books", name: "IX_User_Id", newName: "IX_UserWhoRentedId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Books", name: "IX_UserWhoRentedId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Books", name: "UserWhoRentedId", newName: "User_Id");
        }
    }
}
