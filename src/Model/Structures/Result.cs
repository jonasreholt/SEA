namespace Model.Structures;

using System.Text;

public class Result 
{
    public List<string> QuestionResult {get; set;}

    public int UserId {get; private set;}

    public Result (int userId, List<string> questionResult) {
        QuestionResult = questionResult;
        UserId = userId;
    }

    public override string ToString() {
        return $"{UserId},{Pretty(QuestionResult)}";
    }

    private static string Pretty(List<string> lst)
    {
        var sb = new StringBuilder();
        foreach (var item in lst)
        {
            // We escape all ';' char to as not confuse with our
            // separator, meaning we also escape all escape char ('\')
            // to handle answer ending in such a char i.e. avoiding
            // answer1\;answer2
            // but creating
            // answer1\\;answer2
            var cleanItem = EscapeSpecials(item);
            sb.Append(cleanItem).Append(';');
        }

        // Remove the last ';'
        if (sb.Length > 0) sb.Length--;
        return sb.ToString();
    }

    private static string EscapeSpecials(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        // Assuming we only have to escape a small amount of characters
        // so we pre-allocate same length
        var sb = new StringBuilder(str.Length);

        foreach (var c in str)
        {
            switch (c)
            {
                case ';':
                    sb.Append(@"\;");
                    break;
                case '\\':
                    sb.Append(@"\\");
                    break;
                default:
                    sb.Append(c);
                    break;
            }
        }

        return sb.ToString();
    }
}

//     void StoreResultFromQuestion(int surveyID, int questionsID, int userID, Result result);
