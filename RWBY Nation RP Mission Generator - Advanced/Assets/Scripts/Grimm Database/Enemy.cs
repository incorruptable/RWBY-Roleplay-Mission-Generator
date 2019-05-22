using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RWBYMissionGenerator
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public TerrainFlags Terrain { get; set; }
        public int Rarity { get; set; }
        public int Bounty { get; set; }
    }
}