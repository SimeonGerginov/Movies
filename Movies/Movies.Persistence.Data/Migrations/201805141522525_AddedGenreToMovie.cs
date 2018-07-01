namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedGenreToMovie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            this.AddColumn("dbo.Movies", "GenreId", c => c.Int(nullable: false));
            this.CreateIndex("dbo.Movies", "GenreId");
            this.AddForeignKey("dbo.Movies", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            this.DropIndex("dbo.Movies", new[] { "GenreId" });
            this.DropColumn("dbo.Movies", "GenreId");
            this.DropTable("dbo.Genres");
        }
    }
}
