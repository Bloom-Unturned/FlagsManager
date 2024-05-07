using Microsoft.Extensions.Configuration;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Users;
using SDG.Unturned;
using System.Threading.Tasks;
using OpenMod.Core.Helpers;
using FlagsManager.API.Services;
using FlagsManager.Models.Flags;
using OpenMod.Unturned.Players.Quests.Events;

namespace FlagsManager.Events
{
    public class UnturnedPlayerFlagChangedEventListener : IEventListener<UnturnedPlayerFlagChangedEvent>
    {
        private readonly IUnturnedUserDirectory m_UnturnedUserDirectory;
        private readonly IDbService m_DbService;
        private readonly IConfiguration m_Configuration;
        public UnturnedPlayerFlagChangedEventListener(IDbService dbService, IUnturnedUserDirectory unturnedUserDirectory, IConfiguration configuration)
        {
            m_DbService = dbService;
            m_UnturnedUserDirectory = unturnedUserDirectory;
            m_Configuration = configuration;
         }

        public async Task HandleEventAsync(object sender, UnturnedPlayerFlagChangedEvent @event)
        {
            AsyncHelper.Schedule("UnturnedPlayerFlagChangedEventListener", async () =>
            {
                PlayerQuestFlag flag = @event.Flag;
                if (flag.id != 777 && flag.id != 778) return;
                Flags _flag = new Flags()
                {
                    id = flag.id,
                    value = flag.value,
                    SteamID = @event.Player.SteamId.m_SteamID
                };
                try
                {
                    await m_DbService.AddFlagAsync(_flag);
                }
                catch
                {

                }

            });
        }
    }
}
