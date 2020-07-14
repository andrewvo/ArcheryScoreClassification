namespace ArcheryScoreClassification.Models
{
    public class Request
    {
        public Request(int score, string roundName)
        {
            Score = score;
            RoundName = roundName;
        }
        public int Score { get; set; }
        public string RoundName { get; set; }
    }
}