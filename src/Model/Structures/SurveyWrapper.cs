namespace Model.Structures;



public class SurveyWrapper {

    private int surveyWrapperId;

    private string surveyWrapperName;

    private string[] surveyAssests;

    public int SurveyWrapperId { get => surveyWrapperId;}
    public string SurveyWrapperName { get => surveyWrapperName; set => surveyWrapperName = value;}

    private int current = 0;
    public List<Survey> SurveyVersions = new List<Survey>();

    public SurveyWrapper (int id) {
        surveyWrapperId = id;
        SurveyWrapperName = string.Empty;
        surveyAssests = new string[] {};
        surveyWrapperName = string.Empty;
    }

    public void CopyVersion(int index) {
        // var copiedVersion = surveyVersions[index];
        // surveyVersions.Add(copiedVersion);
    }

    public void Add(Survey survey)
    {
        SurveyVersions.Add(survey);
    }

    public void RemoveAt(int index)
    {
        SurveyVersions.RemoveAt(index);
    }

    public void Remove(Survey survey)
    {
        SurveyVersions.Remove(survey);
    }

    public string[] GetSurveyAssets() {
        return surveyAssests;
    }

    public int GetVersionCount() {
        return SurveyVersions.Count();
    }

    public bool TryGetSurveyVersion(int index, out Survey survey)
    {
        if(0 <= index && index < SurveyVersions.Count) {
            current = index;
            survey = SurveyVersions[index];
            return true;
        }
        survey = default;
        return false;
    }
}