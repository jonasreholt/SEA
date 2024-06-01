namespace Model.Structures;

using System.Collections.Generic;

public class Survey {
    public string SurveyName {get; set;}

    private List<Page> surveyPages = new();

    private int current = -1;

    public Survey() {
        SurveyName = string.Empty;
    }

    public bool PreviousPageExist() => current > 0;
    public bool NextPageExist() => current + 1 < surveyPages.Count;

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
}
