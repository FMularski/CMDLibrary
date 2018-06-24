namespace CMDLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Tragedy')");
            Sql("INSERT INTO Genres (Name) VALUES ('Tragic comedy')");
            Sql("INSERT INTO Genres (Name) VALUES ('Fantasy')");
            Sql("INSERT INTO Genres (Name) VALUES ('Mythology')");
            Sql("INSERT INTO Genres (Name) VALUES ('Adventure')");
            Sql("INSERT INTO Genres (Name) VALUES ('Mystery')");
            Sql("INSERT INTO Genres (Name) VALUES ('Science fiction')");
            Sql("INSERT INTO Genres (Name) VALUES ('Drama')");
            Sql("INSERT INTO Genres (Name) VALUES ('Romance')");
            Sql("INSERT INTO Genres (Name) VALUES ('Action / Adventure')");
            Sql("INSERT INTO Genres (Name) VALUES ('Satire')");
            Sql("INSERT INTO Genres (Name) VALUES ('Horror')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Name = 'Tragedy'");
            Sql("DELETE FROM Genres WHERE Name = 'Tragic comedy'");
            Sql("DELETE FROM Genres WHERE Name = 'Fantasy'");
            Sql("DELETE FROM Genres WHERE Name = 'Mythology'");
            Sql("DELETE FROM Genres WHERE Name = 'Adventure'");
            Sql("DELETE FROM Genres WHERE Name = 'Mystery'");
            Sql("DELETE FROM Genres WHERE Name = 'Science fiction'");
            Sql("DELETE FROM Genres WHERE Name = 'Drama'");
            Sql("DELETE FROM Genres WHERE Name = 'Romance'");
            Sql("DELETE FROM Genres WHERE Name = 'Action / Adventure'");
            Sql("DELETE FROM Genres WHERE Name = 'Satire'");
            Sql("DELETE FROM Genres WHERE Name = 'Horror'");
        }
    }
}
