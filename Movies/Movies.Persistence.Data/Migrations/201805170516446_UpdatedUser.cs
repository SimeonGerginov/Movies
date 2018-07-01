namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedUser : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            this.AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 30));
            this.AddColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
            this.AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.Binary());
        }
        
        public override void Down()
        {
            this.DropColumn("dbo.AspNetUsers", "ProfilePicture");
            this.DropColumn("dbo.AspNetUsers", "Gender");
            this.DropColumn("dbo.AspNetUsers", "LastName");
            this.DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
