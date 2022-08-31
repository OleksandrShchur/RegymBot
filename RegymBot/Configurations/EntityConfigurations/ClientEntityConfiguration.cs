using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegymBot.Data.Entities;

namespace RegymBot.Configurations.EntityConfigurations
{
    public class ClientEntityConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.HasKey(c => c.ClientGuid);
        }
    }
}
