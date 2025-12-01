using Microsoft.EntityFrameworkCore;
using DataBase.Models;
using DataBase.modelBuilder;

namespace DataBase.Context
{

    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)

        {
            Database.EnsureCreated();
        }

        public DbSet<Users_chat> users_chat => Set<Users_chat>();
        public DbSet<Sshkey> sshkeys => Set<Sshkey>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .AddForignKey()
            .AddHasKey();
        }
    }
}