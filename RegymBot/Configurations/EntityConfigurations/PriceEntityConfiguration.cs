using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegymBot.Data.Entities;

namespace RegymBot.Configurations.EntityConfigurations
{
    public class PriceEntityConfiguration : IEntityTypeConfiguration<PriceEntity>
    {
        public void Configure(EntityTypeBuilder<PriceEntity> builder)
        {
            builder.HasKey(p => p.PriceGuid);
        }
    }
}
