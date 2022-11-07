using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegymBot.Data.Entities;
using System;
using System.Reflection;

namespace RegymBot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<CredentialsEntitiy> Credentials { get; set; }
        public DbSet<TGUserEntity> TGUsers { get; set; }
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<PriceEntity> Prices { get; set; }
        public DbSet<ClubEntity> Clubs { get; set; }
        public DbSet<StaticMessageEntity> StaticMessages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<UserClubEntity> UserClubs { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PageEntity> Pages { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<AdminsInfo> AdminsInfo { get; set; }
        public DbSet<AdminsRegistrationLinks> AdminsRegistrationLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyUtcDateTimeConverter();
        }
    }

    public class IgnoreUtcAttribute : Attribute
    {
        public IgnoreUtcAttribute(bool ignore = true)
        {
            IgnoreUtc = ignore;
        }

        public bool IgnoreUtc { get; }
    }
    
    public static class UtcDateAnnotation
    {
        private const string IgnoreUtcAnnotation = "IgnoreUtcConveter";
        private static readonly ValueConverter<DateTime, DateTime> UtcConverter =
            new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        public static PropertyBuilder<TProperty> IgnoreUtc<TProperty>(this PropertyBuilder<TProperty> builder, bool isUtc = true) =>
            builder.HasAnnotation(IgnoreUtcAnnotation, isUtc);

        public static bool IgnoreUtc(this IMutableProperty property) =>
            ((bool?)property.FindAnnotation(IgnoreUtcAnnotation)?.Value) ?? false;

        /// <summary>
        /// Make sure this is called after configuring all your entities.
        /// </summary>
        public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.IgnoreUtc())
                    {
                        continue;
                    }

                    if (property.ClrType == typeof(DateTime) ||
                        property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(UtcConverter);
                    }
                }
            }
        }
    }
}
