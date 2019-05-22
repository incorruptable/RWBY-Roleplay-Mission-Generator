using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RWBYMissionGenerator
{
    public class VillageDatabase
    {
        private List<Village> myVillages;
        private System.Random GetRandom;
        public static VillageDatabase instance = null;
        public static VillageDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VillageDatabase();
                }
                return instance;
            }
        }

        private VillageDatabase()
        {
            this.GetRandom = new System.Random();
            this.myVillages = new List<Village>();

            INIParser ini = new INIParser();
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = filePath + "/RWBYMissionGenerator/config.ini";
            ini.Open(filePath);
            string villageStringNames = ini.ReadValue("Names", "Settlements", "");
            foreach (string name in villageStringNames.Split(','))
            {
                string terrainStrings = ini.ReadValue(name, "Terrain", "");
                TerrainFlags terrain = 0;
                foreach (string terrainString in terrainStrings.Split(','))
                {
                    TerrainFlags result;
                    if (Enum.TryParse(terrainString, true, out result))
                        terrain |= result;
                }

                var village = new Village
                {
                    Name = name,
                    Terrain = terrain,
                    Continent = ini.ReadValue(name, "Continent", ""),
                    Kingdom = ini.ReadValue(name, "Kingdom", "Unaffiliated"),
                    Region = ini.ReadValue(name, "Region", "None"),
                    City = ini.ReadValue(name, "City", "false"),
                };
                this.myVillages.Add(village);
            }
            ini.Close();
        }

        public Village GetRandomVillage(Village toExclude = null)
        {
            if (toExclude == null)
                return this.myVillages[this.GetRandom.Next(0, this.myVillages.Count)];

            return this.myVillages
                .Except(new Village[] { toExclude }).ToList()
                [this.GetRandom.Next(0, this.myVillages.Count - 1)];
        }
    }
}