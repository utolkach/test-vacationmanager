using System.Data.Entity.Migrations;

namespace VacationManager.DomainServices.Migrations
{
    public partial class MadeFieldsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "MiddleName", c => c.String(nullable: false));
            AlterColumn("dbo.Positions", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "Title", c => c.String());
            AlterColumn("dbo.Employees", "MiddleName", c => c.String());
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
        }
    }
}
