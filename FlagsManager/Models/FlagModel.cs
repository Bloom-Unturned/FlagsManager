

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Protocol.Plugins;
using static UnityEngine.UI.GridLayoutGroup;

namespace FlagsManager.Models.Flags
{
    public class Flags
    {
        [Key, Column(Order = 0)]
        [Required]
        public ulong SteamID { get; set; }

        [Key, Column(Order = 1)]
        [Range(1, 2)]
        public ushort id { get; set; }

        [Range(short.MinValue, short.MaxValue)]
        public short value { get; set; }
    }
}