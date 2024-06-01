namespace Model.Structures;



public class SurveyWrapper {

    private string surveyWrapperName;

    private string[] surveyAssests;

    public int PinCode;
    public string SurveyWrapperName { get => surveyWrapperName; set => surveyWrapperName = value;}

    private int current = 0;
    public List<Survey> SurveyVersions = new List<Survey>();

    public SurveyWrapper (int pinCode) {
        PinCode = pinCode;
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

    public SurveyWrapper Copy(int pinCode)
    {
        var copy = new SurveyWrapper(pinCode);
        copy.SurveyWrapperName = surveyWrapperName;
        
        var copySurveyAssets = new string[surveyAssests.Length];
        surveyAssests.CopyTo(copySurveyAssets.AsSpan());
        
        var copyVersions = new List<Survey>(SurveyVersions.Count);
        foreach (var v in SurveyVersions)
        {
            copyVersions.Add(v.Copy());
        }
        copy.SurveyVersions = copyVersions;

        return copy;
    }
}