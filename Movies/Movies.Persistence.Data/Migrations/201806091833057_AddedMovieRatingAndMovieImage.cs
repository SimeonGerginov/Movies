namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedMovieRatingAndMovieImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Rating = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId)
                .Index(t => t.IsDeleted);
            
            AddColumn("dbo.Movies", "Image", c => c.Binary());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int());
            DropColumn("dbo.Movies", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Rating", c => c.Int(nullable: false));
            DropForeignKey("dbo.MovieRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MovieRatings", "MovieId", "dbo.Movies");
            DropIndex("dbo.MovieRatings", new[] { "IsDeleted" });
            DropIndex("dbo.MovieRatings", new[] { "MovieId" });
            DropIndex("dbo.MovieRatings", new[] { "UserId" });
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Movies", "Image");
            DropTable("dbo.MovieRatings");
        }
    }
}
