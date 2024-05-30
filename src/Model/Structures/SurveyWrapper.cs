namespace Model.Structures;



public class SurveyWrapper {

    private int surveyWrapperId;

    private string surveyWrapperName;

    private string[] surveyAssests;

    public int SurveyWrapperId { get => surveyWrapperId;}
    public string SurveyWrapperName { get => surveyWrapperName; set => surveyWrapperName = value;}

    private int current = 0;
    private List<Survey> surveyVersions = new List<Survey>();

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
        surveyVersions.Add(survey);
    }

    public void RemoveAt(int index)
    {
        surveyVersions.RemoveAt(index);
    }

    public void Remove(Survey survey)
    {
        surveyVersions.Remove(survey);
    }

    public string[] GetSurveyAssets() {
        return surveyAssests;
    }

    public int GetVersionCount() {
        return surveyVersions.Count();
    }

    public Survey TryGetModifySurveyVersion(int index)
    {
        if(0 <= index && index < surveyVersions.Count) {
            current = index;
            return surveyVersions[index];
        } else {
            return null;
        }
    }

    public Survey TryGetReadOnlySurveyVersion(int index)
    {
        if(0 <= index && index < surveyVersions.Count) {
            current = index;
            return surveyVersions[index];
        } else {
            return null;
        }
    }
}