using System;
using Microsoft.EntityFrameworkCore;
using OpenMod.EntityFrameworkCore.Configurator;
using OpenMod.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenMod.API.Ioc;
using OpenMod.API.Prioritization;
using FlagsManager.Models.Flags;

namespace FlagsManager.Databases
{
    [PluginServiceImplementation(Lifetime = ServiceLifetime.Singleton, Priority = Priority.Lowest)]
    public class DatabaseContext : OpenModDbContext<DatabaseContext>
    {
        private readonly IServiceProvider m_ServiceProvider;
        public DatabaseContext(IDbContextConfigurator configurator, IServiceProvider serviceProvider) : base(configurator, serviceProvider)
        {
            m_ServiceProvider = serviceProvider;
        }
        private DatabaseContext GetDbContext()
        {
            return m_ServiceProvider.GetRequiredService<DatabaseContext>();
        }
        public DbSet<Flags> Flags => Set<Flags>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flags>()
                .HasKey(f => new { f.SteamID, f.id });
        }
    }
}
