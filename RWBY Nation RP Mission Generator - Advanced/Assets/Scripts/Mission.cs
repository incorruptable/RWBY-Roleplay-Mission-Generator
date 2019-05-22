using System.Text;

namespace RWBYMissionGenerator
{
    public class Mission
    {
        public string Type { get; set; }
        public Village StartVillage { get; set; }
        public Village EndVillage { get; set; }
        public int Payout { get; set; }
        public int Difficulty { get; set; }
        public string GrimmEncounter { get; set; }
        public TargetTypes TargetType { get; set; }
        public string Target { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Mission Type: " + this.Type);

            if (this.GrimmEncounter != "")
                sb.AppendLine("Expected Grimm: " + this.GrimmEncounter);

            if (this.Target != "")
            {
                sb.AppendLine(this.TargetType.ToString() + " Target: " + this.Target);
            }

            if (this.EndVillage == null)
            {
                sb.AppendLine("Nearest Settlement: " + this.StartVillage.Name);
                sb.AppendLine("Continent: " + this.StartVillage.Continent);
                sb.AppendLine("Kingdom: " + this.StartVillage.Kingdom);
                sb.AppendLine("Region: " + this.StartVillage.Region);
            }
            else if (this.StartVillage == null)
            {
                sb.AppendLine("Start Settlement: ERROR");
                sb.AppendLine("Continent: ERROR");
                sb.AppendLine("Kingdom: ERROR");
                sb.AppendLine("Region: ERROR");
            }
            else
            {
                sb.AppendLine("Start Settlement: " + this.StartVillage.Name);
                sb.AppendLine("Continent: " + this.StartVillage.Continent);
                sb.AppendLine("Kingdom: " + this.StartVillage.Kingdom);
                sb.AppendLine("Region: " + this.StartVillage.Region);

                sb.AppendLine("End Settlement: " + this.EndVillage.Name);
                sb.AppendLine("Continent: " + this.EndVillage.Continent);
                sb.AppendLine("Kingdom: " + this.EndVillage.Kingdom);
                sb.AppendLine("Region: " + this.EndVillage.Region);
            }

            string year;

            switch (this.Difficulty)
            {
                case 1:
                    year = "First Year";
                    break;
                case 2:
                    year = "Second Year";
                    break;
                case 3:
                    year = "Third Year";
                    break;
                case 4:
                    year = "Fourth Year";
                    break;
                default:
                    year = "Licensed Hunter";
                    break;
            }
            sb.AppendLine("Recommended Team Minimum: " + year + " Team");

            sb.AppendLine("Mission payout: " + this.Payout);

            return sb.ToString();
        }
    }
}