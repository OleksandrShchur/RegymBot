using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegymBot.Data.Entities;

namespace RegymBot.Configurations.EntityConfigurations
{
    public class CredentialsConfiguration : IEntityTypeConfiguration<CredentialsEntitiy>
    {
        public void Configure(EntityTypeBuilder<CredentialsEntitiy> builder)
        {
            builder.HasKey(c => c.CredentialsGuid);
        }
    }
}
