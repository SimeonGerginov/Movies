namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedMovie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Year = c.String(nullable: false, maxLength: 4),
                        RunningTime = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 200),
                        Rating = c.Int(nullable: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
        }
        
        public override void Down()
        {
            this.DropIndex("dbo.Movies", new[] { "IsDeleted" });
            this.DropTable("dbo.Movies");
        }
    }
}
