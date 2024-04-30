using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityBuilders
{
    public class CardEntityTypeConfiguration
        : IEntityTypeConfiguration<CardEntity>
    {
        public void Configure(EntityTypeBuilder<CardEntity> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
