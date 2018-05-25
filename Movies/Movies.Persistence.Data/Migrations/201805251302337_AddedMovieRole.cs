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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.MovieId);
            
            DropColumn("dbo.People", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Role", c => c.Int(nullable: false));
            DropForeignKey("dbo.MovieRoles", "PersonId", "dbo.People");
            DropForeignKey("dbo.MovieRoles", "MovieId", "dbo.Movies");
            DropIndex("dbo.MovieRoles", new[] { "MovieId" });
            DropIndex("dbo.MovieRoles", new[] { "PersonId" });
            DropTable("dbo.MovieRoles");
        }
    }
}
