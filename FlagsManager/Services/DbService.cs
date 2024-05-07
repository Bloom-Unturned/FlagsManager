// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

using System;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenMod.API.Ioc;
using OpenMod.API.Prioritization;
using System.Linq;
using System.Collections.Generic;
using FlagsManager.Databases;
using FlagsManager.Models.Flags;

namespace FlagsManager.API.Services
{
    [PluginServiceImplementation(Lifetime = ServiceLifetime.Singleton, Priority = Priority.Lowest)]
    public class FlagUpdated : IDbService
    {
        private readonly ILogger<FlagsManager> m_Logger;
        private readonly IServiceProvider m_ServiceProvider;
        public FlagUpdated(IServiceProvider serviceProvider, ILogger<FlagsManager> logger)
        {
            m_ServiceProvider = serviceProvider;
            m_Logger = logger;
            m_Logger.LogInformation("Creating database context");
        }

        public DatabaseContext GetDbContext()
        {
            return m_ServiceProvider.GetRequiredService<DatabaseContext>();
        }
        private async Task RunOperation(Func<DatabaseContext, Task> action)
        {
            await using var dbContext = GetDbContext();

            await action(dbContext);
        }

        private async Task<T> RunOperation<T>(Func<DatabaseContext, Task<T>> action)
        {
            await using var dbContext = GetDbContext();

            return await action(dbContext);
        }

        private void RunOperation(Action<DatabaseContext> action)
        {
            using var dbContext = GetDbContext();

            action(dbContext);
        }

        // upsert
        public async Task AddFlagAsync(Flags flag)
        {
            await RunOperation(async dbContext =>
            {
                List<Flags> existingFlags = await dbContext.Flags.Where(f => f.id == flag.id && f.SteamID == flag.SteamID).ToListAsync();
                if (existingFlags.Any())
                {
                    Flags existingFlag = existingFlags.First(); 
                    existingFlag.SteamID = flag.SteamID;
                    existingFlag.value = flag.value;

                    dbContext.Flags.Update(existingFlag); 
                }
                else
                {
                    await dbContext.Flags.AddAsync(flag);
                }
                await dbContext.SaveChangesAsync();
            });
        }



        public async Task<List<Flags>> GetFlagsAsync(ulong steamid)
        {
            await using var dbContext = GetDbContext();
            ushort[] idsToRetrieve = { 777, 778 };
            return dbContext.Flags.Where(d => d.SteamID == steamid && idsToRetrieve.Contains(d.id)).ToList();
        }

    }
}
