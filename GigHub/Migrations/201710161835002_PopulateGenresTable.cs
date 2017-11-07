namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO genres (Name) Values('Jazz')");
            Sql("INSERT INTO genres (Name) Values('Blues')");
            Sql("INSERT INTO genres (Name) Values('Rock')");
            Sql("INSERT INTO genres (Name) Values('Country')");
        }
        
        public override void Down()
        {
            Sql("Delete FROM Genres Where ID in (1,2,3,4)");
        }
    }
}
