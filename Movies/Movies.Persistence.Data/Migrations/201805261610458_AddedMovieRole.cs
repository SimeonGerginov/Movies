namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedMovieRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.MovieId)
                .Index(t => t.IsDeleted);
            
            this.DropColumn("dbo.People", "Role");
        }
        
        public override void Down()
        {
            this.AddColumn("dbo.People", "Role", c => c.Int(nullable: false));
            this.DropForeignKey("dbo.MovieRoles", "PersonId", "dbo.People");
            this.DropForeignKey("dbo.MovieRoles", "MovieId", "dbo.Movies");
            this.DropIndex("dbo.MovieRoles", new[] { "IsDeleted" });
            this.DropIndex("dbo.MovieRoles", new[] { "MovieId" });
            this.DropIndex("dbo.MovieRoles", new[] { "PersonId" });
            this.DropTable("dbo.MovieRoles");
        }
    }
}
