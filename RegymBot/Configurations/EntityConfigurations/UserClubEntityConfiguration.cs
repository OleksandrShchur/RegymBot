using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegymBot.Data.Entities;

namespace RegymBot.Configurations.EntityConfigurations
{
    public class UserClubEntityConfiguration : IEntityTypeConfiguration<UserClubEntity>
    {
        public void Configure(EntityTypeBuilder<UserClubEntity> builder)
        {
            builder.HasKey(ur => new { ur.UserRef, ur.ClubRef });

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserClubs)
                .HasForeignKey(ur => ur.UserRef);

            builder.HasOne(ur => ur.Club)
                .WithMany(r => r.UserClubs)
                .HasForeignKey(ur => ur.ClubRef);
        }
    }
}
