using System.Collections.Generic;
using System.Threading.Tasks;
using OpenMod.API.Ioc;
using FlagsManager.Databases;
using FlagsManager.Models.Flags;

namespace FlagsManager.API.Services
{
    [Service]
    public interface IDbService
    {
        DatabaseContext GetDbContext();
        Task AddFlagAsync(Flags flag);
        Task<List<Flags>> GetFlagsAsync(ulong steamid);
    }
}
