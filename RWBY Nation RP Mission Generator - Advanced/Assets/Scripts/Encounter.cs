using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace RWBYMissionGenerator
{
    public class Encounter : MonoBehaviour
    {
        [SerializeField]
        Button[] missionControls;
        private Journal journal;

        void Start()
        {
            this.journal = FindObjectOfType<Journal>();
        }

        public void ActivateMissionControls()
        {
            foreach (Button button in missionControls)
            {
                button.interactable = true;
            }
        }

        public void GenerateMissionPD()
        {
            GenerateMission("Perimeter Defence", TargetTypes.None);
        }

        public void GenerateMissionSD()
        {
            GenerateMission("Search and Destroy", TargetTypes.None, false, 10);
        }

        public void GenerateMissionSR()
        {
            GenerateMission("Search and Rescue", TargetTypes.Rescue);
        }

        public void GenerateMissionR()
        {
            GenerateMission("Recovery", TargetTypes.Recovery);
        }

        public void GenerateEscort()
        {
            GenerateMission("Escort", TargetTypes.None, true);
        }

        public void GenerateMissionVS()
        {
            GenerateMission("Village Defence", TargetTypes.None);
        }

        public void GenerateMissionBounty()
        {
            GenerateMission("Bounty", TargetTypes.Bounty);
        }

        public void GeneratePatrol()
        {
            GenerateMission("Patrol", TargetTypes.None, true);
        }

        public void GenerateMissionE()
        {
            GenerateMission("Elimination", TargetTypes.None, false, 50);
        }

        public int MissionPayout(int difficulty, int additionalPay = 0)
        {
            int payout = UnityEngine.Random.Range(50000 * difficulty, 500000 * difficulty);
            payout = payout + additionalPay;
            return payout;
        }

        private int BuyPoints(List<Enemy> grimmList, int points, int difficulty, Village village)
        {
            int year = difficulty;
            int bonusMoney = 0;
            while (points > 0)
            {
                var enemy = this.GetEnemyWeighted(GrimmListFunction());
                if (year <= 2 && enemy.Points <= 2)
                {
                    AddEnemyAndUpdatePoints(grimmList, enemy, ref points, ref bonusMoney, village);
                }
                else if (year > 2)
                {
                    AddEnemyAndUpdatePoints(grimmList, enemy, ref points, ref bonusMoney, village);
                }
            }
            return bonusMoney;
        }

        private void AddEnemyAndUpdatePoints(List<Enemy> grimmList, Enemy enemy, ref int points, ref int bonusMoney, Village village)
        {
            if ((enemy.Terrain & village.Terrain) != 0)
            {
                grimmList.Add(enemy);
                bonusMoney += enemy.Bounty;
                points -= enemy.Points;
            }
            else
            {
                Debug.Log("Water enemy chosen, rerolling");
            }
        }
        

        private List<Enemy> GrimmListFunction()
        {
            /*
             * Create a list of Grimm that utilizes the rarities.
             * nested for loops.
             * exterior loop: Sets the Grimm to be used.
             * interior loop: Takes the Grimm and populates it x times into the list
             */

            List<Enemy> grimmListWeighted = new List<Enemy>();
            var grimmListRaritied = GrimmDatabase.Instance.myEnemies;
            foreach (Enemy Enemy in grimmListRaritied)
            {
                for (int i = 1; i <= Enemy.Rarity; i++)
                {
                    grimmListWeighted.Add(Enemy);
                }
            }
            return grimmListWeighted;
        }

        private Enemy GetEnemyWeighted(List<Enemy> grimmList)
        {
            return grimmList[UnityEngine.Random.Range(0, grimmList.Count)];
        }

        private string PrettifyGrimmString(List<Enemy> grimmList)
        {
            var grimmArrayPretty = new List<string>();
            var groups = grimmList.GroupBy(x => x.Name);
            foreach (var group in groups)
            {
                var grimm = group.Key;
                var total = group.Count();

                grimmArrayPretty.Add(grimm + " x" + total);
            }
            grimmArrayPretty.Sort();
            return string.Join(", ", grimmArrayPretty);
        }

        private void GenerateMission(string type, TargetTypes targetType, bool hasTwoVillages = false, int pointModifier = 0)
        {
            int difficulty = UnityEngine.Random.Range(1, 5);
            Village village = null;
            if (type == "Perimeter Defence")
            {
                bool notCity = true;
                do
                {
                    village = VillageDatabase.Instance.GetRandomVillage();
                    int count = 100;
                    if (village.City == "true")
                    {
                        notCity = false;
                    }
                    count -= 1;
                    if(count == 0 && village.City == "true")
                    {
                        notCity = false;
                        village = null;
                    }
                } while (notCity == true);
            }
            else
            {
                village = VillageDatabase.Instance.GetRandomVillage();
            }
            Village secondVillage = null;
            List<Enemy> grimmList = new List<Enemy>();
            int points = pointModifier * difficulty;
            int bonusMoney = BuyPoints(grimmList, points, difficulty, village);
            string combinedString = PrettifyGrimmString(grimmList);
            string target = "";
            switch (targetType)
            {
                case TargetTypes.Bounty:
                case TargetTypes.Rescue:
                    target = GenerateNames.Instance.GetRandomName();
                    break;
                case TargetTypes.Recovery:
                    int recoveryTarget = UnityEngine.Random.Range(1, 6);
                    switch (recoveryTarget)
                    {
                        case 1:
                            target = "Food Supplies";
                            //Food Supplies
                            break;
                        case 2:
                            target = "Munitions";
                            //Weapons
                            break;
                        case 3:
                            target = "Dust";
                            //Dust
                            break;
                        case 4:
                            target = "Live Stock";
                            //Live Stock
                            break;
                        case 5:
                            target = "Building Supplies";
                            //Building Supplies
                            break;
                        case 6:
                            target = "Raw Materials";
                            //Raw Materials
                            break;
                        default:
                            target = "Error. Please Contact Developer";
                            break;
                    }
                    break;
                case TargetTypes.None:
                    break;
            }

            if (hasTwoVillages)
            {
                secondVillage = VillageDatabase.Instance.GetRandomVillage(village);
            }

            var mission = new Mission
            {
                Type = type,
                StartVillage = village,
                EndVillage = secondVillage,
                GrimmEncounter = combinedString,
                Difficulty = difficulty,
                Payout = MissionPayout(difficulty, bonusMoney),
                Target = target,
                TargetType = targetType,
            };

            var output = mission.ToString();
            this.journal.Log(output);
            MissionWriter.Instance.WriteToFile(output);
        }
    }
}