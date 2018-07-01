namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedPerson : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 30));
            this.AddColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 30));
            this.AddColumn("dbo.People", "Age", c => c.Int(nullable: false));
            this.AddColumn("dbo.People", "Gender", c => c.Int(nullable: false));
            this.AddColumn("dbo.People", "Picture", c => c.Binary());
            this.AddColumn("dbo.People", "DateOfBirth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            this.DropColumn("dbo.People", "Name");
            this.DropColumn("dbo.People", "Birthday");
        }
        
        public override void Down()
        {
            this.AddColumn("dbo.People", "Birthday", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            this.AddColumn("dbo.People", "Name", c => c.String(nullable: false, maxLength: 30));
            this.DropColumn("dbo.People", "DateOfBirth");
            this.DropColumn("dbo.People", "Picture");
            this.DropColumn("dbo.People", "Gender");
            this.DropColumn("dbo.People", "Age");
            this.DropColumn("dbo.People", "LastName");
            this.DropColumn("dbo.People", "FirstName");
        }
    }
}
