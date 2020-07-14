using System.Collections.Generic;
using System.Security;

namespace ArcheryScoreClassification.Helpers
{
    public interface IGetClosestClassification
    {
        string Get(int score, Dictionary<string, int> classificationScores);
    }
}
