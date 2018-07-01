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
            
            this.AddColumn("dbo.Movies", "Image", c => c.Binary());
            this.AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 30));
            this.AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 30));
            this.AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int());
            this.DropColumn("dbo.Movies", "Rating");
        }
        
        public override void Down()
        {
            this.AddColumn("dbo.Movies", "Rating", c => c.Int(nullable: false));
            this.DropForeignKey("dbo.MovieRatings", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.MovieRatings", "MovieId", "dbo.Movies");
            this.DropIndex("dbo.MovieRatings", new[] { "IsDeleted" });
            this.DropIndex("dbo.MovieRatings", new[] { "MovieId" });
            this.DropIndex("dbo.MovieRatings", new[] { "UserId" });
            this.AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
            this.AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 30));
            this.AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            this.DropColumn("dbo.Movies", "Image");
            this.DropTable("dbo.MovieRatings");
        }
    }
}
