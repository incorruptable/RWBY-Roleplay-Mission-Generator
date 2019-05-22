using System;
namespace RWBYMissionGenerator
{
    [Flags]
    public enum TerrainFlags
    {
        None = 0,
        Land = 1,
        Water = 2,
        Air = 4,
        Snow = 8,
        Desert = 16,
        Forest = 32,
        Mountain = 64,
    }

    public enum TargetTypes
    {
        None = 0,
        Recovery = 1,
        Rescue = 2,
        Bounty = 3,
    }
}