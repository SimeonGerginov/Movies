namespace Movies.Persistence.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.People", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.People", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.People", "Picture", c => c.Binary());
            AddColumn("dbo.People", "DateOfBirth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.People", "Name");
            DropColumn("dbo.People", "Birthday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Birthday", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.People", "Name", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.People", "DateOfBirth");
            DropColumn("dbo.People", "Picture");
            DropColumn("dbo.People", "Gender");
            DropColumn("dbo.People", "Age");
            DropColumn("dbo.People", "LastName");
            DropColumn("dbo.People", "FirstName");
        }
    }
}
