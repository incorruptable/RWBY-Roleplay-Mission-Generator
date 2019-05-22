using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RWBYMissionGenerator
{
    public class GenerateNames
    {
        private static GenerateNames instance = null;
        private string PATH = string.Format("{0}/RWBYMissionGenerator/textFiles/AvailableNames.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        private System.Random GetRandom;

        public static GenerateNames Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GenerateNames();
                }
                return instance;
            }
        }


        private GenerateNames()
        {
            this.GetRandom = new System.Random();

            if (!File.Exists(PATH))
                this.GenerateNamesList();
        }

        public string GetRandomName()
        {
            List<string> nameList = ReadFromFile(PATH);

            if (nameList.Count == 0)
                return "ERROR - No names exist in file";

            string chosenName = nameList[this.GetRandom.Next(0, nameList.Count)];
            nameList.Remove(chosenName);
            File.WriteAllText(PATH, String.Empty);
            using (StreamWriter writer = File.AppendText(PATH))
            {
                foreach (string name in nameList)
                    writer.WriteLine(name, PATH);
            }

            return chosenName;
        }

        private void GenerateNamesList()
        {
            File.CreateText(PATH).Dispose();

            List<string> firstNames = ReadFromFile(string.Format("{0}/RWBYMissionGenerator/textFiles/FirstNames.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
            firstNames.Sort();
            List<string> lastNames = ReadFromFile(string.Format("{0}/RWBYMissionGenerator/textFiles/LastNames.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
            lastNames.Sort();

            using (StreamWriter writer = File.AppendText(PATH))
            {
                foreach (string firstName in firstNames)
                {
                    foreach (string lastName in lastNames)
                    {
                        if ((firstName != lastName) && (lastName != "") && (firstNames.Contains(lastName) == false))
                        {
                            string name = string.Format("{0} {1}", firstName, lastName);
                            Debug.Log(name);
                            writer.WriteLine(name);
                        }
                    }
                }
            }
        }

        public List<string> ReadFromFile(string fileName)
        {
            string line;
            List<string> lines = new List<string>();
            if (!File.Exists(fileName))
                Debug.Log("File " + fileName + " does not exist or is unreadable.");

            using (StreamReader file = new StreamReader(fileName))
                while ((line = file.ReadLine()) != null)
                    lines.Add(line);

            return lines;
        }
    }
}
