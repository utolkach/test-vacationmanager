using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VacationManager.DomainServices.Entities;

namespace VacationManager.DomainServices.Mapping
{
    public class PositionMapping : EntityTypeConfiguration<Position>
    {
        public PositionMapping()
        {
            HasKey(x => x.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.Title).IsRequired().HasMaxLength(100);
        }
    }
}