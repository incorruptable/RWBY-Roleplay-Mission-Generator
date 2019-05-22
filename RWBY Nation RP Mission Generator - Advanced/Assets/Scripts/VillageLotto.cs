using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RWBYMissionGenerator
{
    public class VillageLotto : MonoBehaviour
    {
        public Village Village { get; set; }
        // Use this for initialization
        public VillageLotto()
        {
            
        }

        public Village LottoVillage()
        {
            Village = VillageDatabase.Instance.GetRandomVillage();
            return Village;
        }
    }
}