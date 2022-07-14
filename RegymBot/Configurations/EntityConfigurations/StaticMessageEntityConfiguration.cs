using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegymBot.Data.Entities;

namespace RegymBot.Configurations.EntityConfigurations
{
    public class StaticMessageEntityConfiguration : IEntityTypeConfiguration<StaticMessageEntity>
    {
        public void Configure(EntityTypeBuilder<StaticMessageEntity> builder)
        {
            builder.HasKey(m => m.StaticMessageGuid);
        }
    }
}
