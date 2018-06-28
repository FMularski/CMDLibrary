namespace CMDLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRentedDate_ReturnTermColumnsToBooksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "RentedDate", c => c.DateTime());
            AddColumn("dbo.Books", "ReturnTerm", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ReturnTerm");
            DropColumn("dbo.Books", "RentedDate");
        }
    }
}
