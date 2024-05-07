using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Users;
using SDG.Unturned;
using System.Threading.Tasks;
using FlagsManager.API.Services;
using OpenMod.Unturned.Players.Connections.Events;
using NuGet.Protocol;
using FlagsManager.Models.Flags;
using System.Collections.Generic;
using System.Linq;
using OpenMod.API.Permissions;

namespace FlagsManager.Events
{
    public class UnturnedPlayerConnectedEventListener : IEventListener<UnturnedPlayerConnectedEvent>
    {
        private readonly IUnturnedUserDirectory m_UnturnedUserDirectory;
        private readonly IPermissionChecker m_PermissionChecker;
        private readonly IDbService m_DbService;
        private readonly ILogger<UnturnedPlayerConnectedEventListener> m_Logger;

        public UnturnedPlayerConnectedEventListener(ILogger<UnturnedPlayerConnectedEventListener> logger, IPermissionChecker permissionChecker, IDbService dbService, IUnturnedUserDirectory unturnedUserDirectory)
        {
            m_PermissionChecker = permissionChecker;
            m_DbService = dbService;
            m_UnturnedUserDirectory = unturnedUserDirectory;
            m_Logger = logger;
        }

        public async Task HandleEventAsync(object sender, UnturnedPlayerConnectedEvent @event)
        {
            Player player = @event.Player.Player;

                player.quests.getFlag(778, out short width);
                player.quests.getFlag(777, out short height);

            List<Flags> flags = await m_DbService.GetFlagsAsync(@event.Player.SteamId.m_SteamID);
                Flags flag777 = flags.FirstOrDefault(f => f.id == 777);
                Flags flag778 = flags.FirstOrDefault(f => f.id == 778);

            if (width == 0 || height == 0)
            {

                if (flag777 != null && flag778 != null)
                {
                    player.quests.setFlag(777, flag777.value);
                    player.quests.setFlag(778, flag778.value);
                }
                else
                {
                    player.quests.setFlag(777, 8);
                    player.quests.setFlag(778, 8);
                }
            }
            else
            {
                if (flag777.value != height) player.quests.setFlag(777, flag777.value);
                if (flag778.value != width) player.quests.setFlag(778, flag778.value);
            }
        }

    }
}