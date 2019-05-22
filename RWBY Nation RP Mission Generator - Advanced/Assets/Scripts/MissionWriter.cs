using System;
using System.IO;

namespace RWBYMissionGenerator
{
    public class MissionWriter
    {
        private static MissionWriter instance = null;
        private DateTime timeStored;

        public static MissionWriter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MissionWriter();
                }
                return instance;
            }
        }

        private MissionWriter()
        {
            this.timeStored = DateTime.Now;
        }

        public void WriteToFile(string textToWrite)
        {
            var fileName = string.Format("{0}/RWBYMissionGenerator/missions/Missions-{1}.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), this.timeStored.ToString("yyyy-MM-dd-HH-mm"));
            WriteToFile(textToWrite, fileName);
        }

        private void WriteToFile(string textToWrite, string fileName)
        {
            if (!File.Exists(fileName))
                File.CreateText(fileName).Dispose();
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(textToWrite);
            }
        }
    }
}