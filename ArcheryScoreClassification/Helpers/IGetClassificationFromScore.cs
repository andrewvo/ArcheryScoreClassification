namespace ArcheryScoreClassification.Helpers
{
    public interface IGetClassificationFromScore
    {
        string GetClassification(int score, string roundName);
    }
}