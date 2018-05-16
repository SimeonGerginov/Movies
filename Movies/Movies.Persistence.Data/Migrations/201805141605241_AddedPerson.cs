namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedPerson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Nationality = c.String(nullable: false, maxLength: 30),
                        Birthday = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Role = c.Int(nullable: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.PersonMovie",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.MovieId })
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.MovieId);  
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonMovie", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.PersonMovie", "PersonId", "dbo.People");
            DropIndex("dbo.PersonMovie", new[] { "MovieId" });
            DropIndex("dbo.PersonMovie", new[] { "PersonId" });
            DropIndex("dbo.People", new[] { "IsDeleted" });
            DropTable("dbo.PersonMovie");
            DropTable("dbo.People");
        }
    }
}
