namespace Model.Survey;

using System.Collections.Generic;

public interface IModifySurveyWrapper {
    IModifySurvey TryGetModifySurveyVersion(int index); // Return survey index'

    IModifySurvey AddNewVersion();

    int GetVersionCount(); // Return number of versions

    void CopyVersion(int index);

    void DeleteVersion(int index);
    string[] GetSurveyAssets(); // Get pictures from the survey

    string SurveyWrapperName {get; set;}
}