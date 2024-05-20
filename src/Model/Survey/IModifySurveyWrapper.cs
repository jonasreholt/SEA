namespace Model.Survey;

using System.Collections.Generic;

public interface IModifySurveyWrapper {

    IModifySurveyWrapper TryGetModifySurveyWrapper(int surveyId); // <SurveyA,SurveyB>

    IModifySurvey TryGetModifySurveyVersion(int index); // Return survey index'

    int GetVersionCount(); // Return number of versions

    void CopyVersion(int index);

    void DeleteVersion(int index);
    string[] GetSurveyAssets(); // Get pictures from the survey
}