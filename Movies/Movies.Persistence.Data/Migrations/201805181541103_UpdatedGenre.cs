namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedGenre : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Genres", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            this.AddColumn("dbo.Genres", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            this.AddColumn("dbo.Genres", "IsDeleted", c => c.Boolean(nullable: false));
            this.AddColumn("dbo.Genres", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            this.CreateIndex("dbo.Genres", "IsDeleted");
        }
        
        public override void Down()
        {
            this.DropIndex("dbo.Genres", new[] { "IsDeleted" });
            this.DropColumn("dbo.Genres", "DeletedOn");
            this.DropColumn("dbo.Genres", "IsDeleted");
            this.DropColumn("dbo.Genres", "ModifiedOn");
            this.DropColumn("dbo.Genres", "CreatedOn");
        }
    }
}
