using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VacationManager.DomainServices.Entities;

namespace VacationManager.DomainServices.Mapping
{
    public class EmployeeMapping : EntityTypeConfiguration<Employee>
    {
        public EmployeeMapping()
        {
            ToTable("Employees");
            HasKey(x => x.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            Property(x => x.MiddleName).IsRequired().HasMaxLength(100);
            Property(x => x.LastName).IsRequired().HasMaxLength(100);

            HasOptional(x => x.Vacations);
            HasRequired(x => x.Position).WithMany().HasForeignKey(x => x.PositionId);
        }
    }
}