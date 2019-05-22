using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RWBYMissionGenerator
{
    public class Journal : MonoBehaviour
    {
        [SerializeField] Text logText;

        public void Log(string text)
        {
            logText.text += "\n" + text;
        }
    }
}