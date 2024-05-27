namespace Model.Survey;
using System.Collections.Generic;

public interface IReadOnlySurveyWrapper {

    // List<SurveyA,SurveyB>

    IReadOnlySurvey TryGetReadOnlySurveyVersion(int index); // Return survey index'

    int GetVersionCount(); // Return number of versions

    string SurveyWrapperName { get; }
    int SurveyWrapperId { get; }

}
