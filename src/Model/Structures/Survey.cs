using System.Text.Json.Serialization;

namespace Model.Structures;

using System.Collections.Generic;

public class Survey 
{
    [JsonInclude]
    public string SurveyName {get; set;}

    [JsonInclude]
    private List<Page> surveyPages = new();

    private int current = -1;

    public Survey() 
    {
        SurveyName = string.Empty;
    }

    public List<Dictionary<int, Result>> GetResults()
    {
        var results = new List<Dictionary<int, Result>>();
        foreach (var page in surveyPages)
        {
            foreach (var question in page)
            {
                foreach (var subQuestion in question.SubQuestions)
                {
                    results.Add(subQuestion.Results);
                }
            }
        }

        return results;
    }

    public void ClearResults()
    {
        foreach (var page in surveyPages)
        {
            foreach (var question in page)
            {
                foreach (var subQuestion in question.SubQuestions)
                {
                    subQuestion.Results.Clear();
                }
            }
        }
    }

    public bool PreviousPageExist() => current > 0;
    public bool NextPageExist() => current + 1 < surveyPages.Count;

    public List<KeyValuePair<string, string>> FixImagePaths(string newDir)
    {
        var counter = 0;
        var pathToNewPath = new Dictionary<string, string>();
        var seenFileNames = new HashSet<string>();

        foreach (var page in surveyPages)
        {
            foreach (var question in page)
            {
                var path = question.PicturePath;
                if (string.IsNullOrEmpty(path)) continue;
                
                // This is a complete duplicate image, so it's already saved
                if (pathToNewPath.ContainsKey(path)) continue;
                var name = Path.GetFileName(path);
                // we have a duplicate file name so fix it
                if (seenFileNames.Contains(name))
                {
                    name = $"{counter++}_{name}";
                }

                var newPath = Path.Combine(newDir, name);
                pathToNewPath[path] = newPath;
                seenFileNames.Add(name);

                question.PicturePath = newPath;
            }
        }

        return pathToNewPath.ToList();
    }

    public Page? GetNextPage()
    {
        return NextPageExist()
            ? surveyPages[++current]
            : null;
    }

    public Page? GetPreviousPage()
    {
        return PreviousPageExist()
            ? surveyPages[--current]
            : null;
    }

    public Page? GetCurrent()
    {
        return current != -1
            ? surveyPages[current]
            : null;
    }

    public void ResetCounter()
    {
        current = -1;
    }

    public void DeletePage(int index) {
        if(0 < current && current < surveyPages.Count) {
            surveyPages.RemoveAt(index);
        }
    }

    public void Add(Page page) 
    {
        surveyPages.Add(page);
    }

    public void Insert(int index, Page page)
    {
        surveyPages.Insert(index, page);
    }

    public Survey Copy()
    {
        var copy = new Survey();
        copy.SurveyName = SurveyName;

        copy.surveyPages.Capacity = surveyPages.Count;
        foreach (var p in surveyPages)
        {
            copy.surveyPages.Add(p.Copy());
        }
        return copy;
    }
}
