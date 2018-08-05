using System.Data.Entity.Migrations;

namespace VacationManager.DomainServices.Migrations
{
    public partial class SetMaxLengthToStringFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Employees", "MiddleName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Positions", "Title", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "MiddleName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false));
        }
    }
}
