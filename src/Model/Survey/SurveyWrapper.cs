namespace Model.Survey;



public class SurveyWrapper : IReadOnlySurveyWrapper, IModifySurveyWrapper {

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

    public void DeleteVersion(int index) {
        surveyVersions.RemoveAt(index);
    }

    public string[] GetSurveyAssets() {
        return surveyAssests;
    }

    public int GetVersionCount() {
        return surveyVersions.Count();
    }

    public IModifySurvey TryGetModifySurveyVersion(int index)
    {
        if(0 <= index && index < (surveyVersions.Count() - 1)) { 
            current = index;
            return surveyVersions[index];
        } else {
            return null;
        }
    }

    public IReadOnlySurvey TryGetReadOnlySurveyVersion(int index)
    {
        if(0 <= index && index < (surveyVersions.Count() - 1)) { 
            current = index;
            return surveyVersions[index];
        } else {
            return null;
        }
    }
}