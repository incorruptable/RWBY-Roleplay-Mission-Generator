using UnityEngine;

namespace RWBYMissionGenerator
{
    public class Initial : MonoBehaviour
    {
        void Start()
        {
            var journal = FindObjectOfType<Journal>();
            journal.Log("There are a few things to note.");
            journal.Log("If something displays as an Error, please report it to the developer immediately.");
            journal.Log("You will need to explain what happened to generate the error. He may ask for your config file. Please make sure to provide that to him if necessary.");
            journal.Log("Some missions, will require a GM to run them.\nThose include: Escort, Bounty, Search and Rescue, and Recovery.\n\n\n\n\n\n\n");
        }
    }
}