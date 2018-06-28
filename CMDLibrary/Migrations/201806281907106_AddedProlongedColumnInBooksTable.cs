namespace CMDLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProlongedColumnInBooksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Prolonged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Prolonged");
        }
    }
}
