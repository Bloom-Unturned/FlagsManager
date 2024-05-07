using System;
using Cysharp.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.EntityFrameworkCore.Configurator;
using OpenMod.EntityFrameworkCore;
using OpenMod.Unturned.Plugins;
using Microsoft.Extensions.DependencyInjection;
using OpenMod.API.Ioc;
using OpenMod.API.Prioritization;
using OpenMod.Core.Commands;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Threading;
using SDG.Unturned;
using OpenMod.Unturned.Users;
using Command = OpenMod.Core.Commands.Command;
using FlagsManager.API.Services;
using System.Linq;
using OpenMod.API.Commands;
using OpenMod.API.Permissions;
using FlagsManager.Events;
using Steamworks;
using FlagsManager.Models.Flags;

namespace FlagsManager.Commands
{
    [Command("flag2")]
    [CommandDescription("gives an online player a flag")]
    public class FlagCommand : Command
    {
        private readonly IDbService m_DbService;
        private readonly IUnturnedUserDirectory m_UnturnedUserDirectory;

        public FlagCommand(IServiceProvider serviceProvider, IDbService dbService, IUnturnedUserDirectory unturnedUserDirectory) : base(serviceProvider)
        {
            m_DbService = dbService;
            m_UnturnedUserDirectory = unturnedUserDirectory;
        }
        protected override async Task OnExecuteAsync()
        {
            var steamid = await Context.Parameters.GetAsync<CSteamID>(0);
            string dm = await Context.Parameters.GetAsync<string>(1);
            short value = await Context.Parameters.GetAsync<short>(2);
            ushort flag = 0;

            UnturnedUser user = m_UnturnedUserDirectory.FindUser(steamid);
            if (dm == "w") flag = 777;
            else if (dm == "h") flag = 778;

            Flags _flag = new Flags()
            {
                id = flag,
                value = value,
                SteamID = steamid.m_SteamID
            };
            try
            {
                await m_DbService.AddFlagAsync(_flag);
            }
            catch
            {

            }
        }
    }
}
