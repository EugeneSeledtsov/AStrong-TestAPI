using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityBuilders
{
    public class FileEntityTypeConfiguration : IEntityTypeConfiguration<FileEntity>
    {
        public void Configure(EntityTypeBuilder<FileEntity> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder.HasOne(t => t.Card).WithOne(t => t.Picture);
        }
    }
}
