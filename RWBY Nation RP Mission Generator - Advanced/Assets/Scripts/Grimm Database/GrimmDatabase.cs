using System;
using System.Collections.Generic;

namespace RWBYMissionGenerator
{
    public class GrimmDatabase
    {
        private static GrimmDatabase instance = null;
        public List<Enemy> myEnemies;
        private System.Random GetRandom;

        public static GrimmDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GrimmDatabase();
                }
                return instance;
            }
        }

        private GrimmDatabase()
        {
            this.GetRandom = new System.Random();
            this.myEnemies = new List<Enemy>();

            INIParser ini = new INIParser();
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = filePath + "/RWBYMissionGenerator/config.ini";
            ini.Open(filePath);
            string enemyStringNames = ini.ReadValue("Names", "Enemies", "");
            foreach (string name in enemyStringNames.Split(','))
            {
                string terrainStrings = ini.ReadValue(name, "Terrain", "");
                TerrainFlags terrain = 0;
                foreach (string terrainString in terrainStrings.Split(','))
                {
                    TerrainFlags result;
                    if (Enum.TryParse(terrainString, true, out result))
                        terrain |= result;
                }

                var enemy = new Enemy
                {
                    Name = name,
                    Terrain = terrain,
                    Rarity = ini.ReadValue(name, "Rarity", 0),
                    Points = ini.ReadValue(name, "Points", 0),
                    Bounty = ini.ReadValue(name, "Bounty", 0)
                };
                this.myEnemies.Add(enemy);
            }
            ini.Close();
        }

        public Enemy GetRandomEnemy()
        {
            return this.myEnemies[this.GetRandom.Next(0, this.myEnemies.Count)];
        }
    }
}