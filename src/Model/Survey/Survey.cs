namespace Model.Survey;
using System;

using Model.Question;
using Model.Answer;
using System.Collections.Generic;

internal class Survey : IReadOnlySurvey, IModifySurvey {
    public int SurveyId {get;}

    public string SurveyName {get; set;}

    private List<List<Question>> surveyQuestions = new List<List<Question>>();

    private int current = 0;

    public Survey(int surveyId) {
        SurveyId = surveyId;
        SurveyName = string.Empty;
    }

    public bool PreviousQuestionExist() => throw new NotImplementedException();
    public bool NextQuestionExist() => throw new NotImplementedException();

    public IEnumerable<IReadOnlyQuestion>? TryGetNextReadOnlyQuestion() {
        if(0 <= current && current < (surveyQuestions.Count() - 1)) {
            current++;
            return surveyQuestions[current];
        } else {
            return null;
        }
    }

    public IEnumerable<IReadOnlyQuestion>? TryGetPreviousReadOnlyQuestion()
    {
        if(0 < current && current < (surveyQuestions.Count())) {
            current--;
            return surveyQuestions[current];
        } else {
            return null;
        }
    }

    public IEnumerable<IModifyQuestion>? TryGetModifyQuestion(int index) {
        if(0 <= index && index < (surveyQuestions.Count() - 1)) {
            current = index;
            return surveyQuestions[index];
        } else {
            return null;
        }
    }
    public IEnumerable<IModifyQuestion>? TryGetNextModifyQuestion()
    {
        if(0 <= current && current < (surveyQuestions.Count() - 1)) {
            current++;
            return surveyQuestions[current];
        } else {
            return null;
        }
    }

    public IEnumerable<IModifyQuestion>? TryGetPreviousModifyQuestion() {
        if(0 < current && current < (surveyQuestions.Count())) {
            current--;
            return surveyQuestions[current];
        } else {
            return null;
        }
    }

    public void DeleteQuestion(int index) {
        if(0 < current && current < (surveyQuestions.Count())) {
            surveyQuestions.RemoveAt(index);
        }
    }

    public IEnumerable<Question> AddNewQuestion() {
        List<Question> result = new List<Question>();
        surveyQuestions.Add(result);
        return result;
    }

    public IEnumerable<Question> InsertNewQuestion(int index)
    {
        List<Question> result = new List<Question>();
        surveyQuestions.Insert(index, result);
        return result;
    }
}
