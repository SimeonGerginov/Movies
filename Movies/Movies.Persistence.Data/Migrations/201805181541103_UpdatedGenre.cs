namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedGenre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Genres", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Genres", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Genres", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Genres", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.Genres", "IsDeleted");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Genres", new[] { "IsDeleted" });
            DropColumn("dbo.Genres", "DeletedOn");
            DropColumn("dbo.Genres", "IsDeleted");
            DropColumn("dbo.Genres", "ModifiedOn");
            DropColumn("dbo.Genres", "CreatedOn");
        }
    }
}
