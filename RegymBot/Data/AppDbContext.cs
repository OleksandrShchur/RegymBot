using Microsoft.EntityFrameworkCore;
using RegymBot.Configurations.EntityConfigurations;
using RegymBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RegymBot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<HistoryEntity> Histories { get; set; }
        public DbSet<PriceEntity> Prices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
