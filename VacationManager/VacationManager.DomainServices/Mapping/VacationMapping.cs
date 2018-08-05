using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VacationManager.DomainServices.Entities;

namespace VacationManager.DomainServices.Mapping
{
    public class VacationMapping : EntityTypeConfiguration<Vacation>
    {
        public VacationMapping()
        {
            ToTable("Vacations");
            HasKey(x => x.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.Start).IsRequired();
            Property(x => x.End).IsRequired();

            HasRequired(x => x.Employee).WithMany(x => x.Vacations).HasForeignKey(x => x.EmployeeId);
        }
    }
}