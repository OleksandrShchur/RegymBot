using Microsoft.EntityFrameworkCore;
using RegymBot.Data.Entities;
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
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<PriceEntity> Prices { get; set; }
        public DbSet<StaticMessageEntity> StaticMessages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PageEntity> Pages { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
