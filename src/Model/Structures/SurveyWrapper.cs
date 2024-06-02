using System.Text.Json.Serialization;

namespace Model.Structures;



public class SurveyWrapper 
{
    [JsonInclude]
    private string surveyWrapperName;

    [JsonInclude]
    public int PinCode;
    public string SurveyWrapperName { get => surveyWrapperName; set => surveyWrapperName = value;}
    
    [JsonInclude]
    public List<Survey> SurveyVersions = new ();

    public SurveyWrapper (int pinCode) {
        PinCode = pinCode;
        SurveyWrapperName = string.Empty;
        surveyWrapperName = string.Empty;
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

    public int GetVersionCount() {
        return SurveyVersions.Count();
    }

    public bool TryGetSurveyVersion(int index, out Survey survey)
    {
        if(0 <= index && index < SurveyVersions.Count) {
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
        
        var copyVersions = new List<Survey>(SurveyVersions.Count);
        foreach (var v in SurveyVersions)
        {
            copyVersions.Add(v.Copy());
        }
        copy.SurveyVersions = copyVersions;

        return copy;
    }
}