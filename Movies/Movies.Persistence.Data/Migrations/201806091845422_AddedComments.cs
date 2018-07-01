namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false, maxLength: 200),
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
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Comments", "MovieId", "dbo.Movies");
            this.DropIndex("dbo.Comments", new[] { "IsDeleted" });
            this.DropIndex("dbo.Comments", new[] { "MovieId" });
            this.DropIndex("dbo.Comments", new[] { "UserId" });
            this.DropTable("dbo.Comments");
        }
    }
}
