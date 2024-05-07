using System;
using Cysharp.Threading.Tasks;
using HarmonyLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Permissions;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;
using FlagsManager.Databases;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

[assembly: PluginMetadata("FlagsManager", DisplayName = "FlagsManager", Author = "Hath.")]

namespace FlagsManager
{
    public class FlagsManager : OpenModUnturnedPlugin
    {
        private Harmony harmony;
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;
        private readonly ILogger<FlagsManager> m_Logger;
        private readonly IServiceProvider m_ServiceProvider;
        private readonly IPermissionRegistry m_PermissionRegistry;

        public FlagsManager(
            IConfiguration configuration,
            IStringLocalizer stringLocalizer,
            ILogger<FlagsManager> logger,
            IPermissionRegistry permissionRegistry,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
            m_ServiceProvider = serviceProvider;
            m_Logger = logger;
            m_PermissionRegistry = permissionRegistry;
        }

        protected override async UniTask OnLoadAsync()
        {
            await using var dbContext = m_ServiceProvider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.MigrateAsync();
        }

        protected override async UniTask OnUnloadAsync()
        {
            m_Logger.LogInformation(m_StringLocalizer["plugin_events:plugin_stop"]);
        }
    }
}
