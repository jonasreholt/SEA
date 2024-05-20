using System;
namespace Model.Survey;

using Model.Question;
using Model.Answer;


public class Survey : IReadOnlySurvey, IModifySurvey {

    public IReadOnlyAnswer ReadOnlyAnswer {get;}
    
    public IModifyAnswer ModifyAnswer {get; set;}   
    private int current;    
    public int surveyId {get;}
    public Survey(int surveyId) { 
        this.surveyId = surveyId;
    }

}
    // /// <summary>
    // /// Tries to get version A or B of a question pair. Returns null if the last
    // /// question has been reached.
    // /// </summary>
    // /// <param name="questionAorB">question A="QuestionA" and question B="QuestionB"</param>
    // public IReadOnlyQuestion? TryGetNextReadOnlyQuestion(QuestionVersion questionVersion) {
    //     if(current < (surveyQuestions.Count() - 1)) { 
    //         current++;
    //         if (questionVersion == QuestionVersion.QuestionA)
    //             return surveyQuestions[current].QuestionA;
    //         else
    //             return surveyQuestions[current].QuestionB;
    //     } else{
    //         current = surveyQuestions.Count();
    //         return null;
    //     }  
    // }

    // public IModifyQuestion? TryGetNextModifyQuestion(QuestionVersion questionVersion) {
    //     if(current < (surveyQuestions.Count() - 1)) { 
    //         current++;
    //         if (questionVersion == QuestionVersion.QuestionA)
    //             return surveyQuestions[current].QuestionA;
    //         else
    //             return surveyQuestions[current].QuestionB;
    //     } else{
    //         current = surveyQuestions.Count();
    //         return null;
    //     }  
    // }

    // public IReadOnlyQuestion? TryGetPreviousReadOnlyQuestion(QuestionVersion questionVersion) {
    //     if (current > 0) { 
    //         current--;
    //         if (questionVersion == QuestionVersion.QuestionA)
    //             return surveyQuestions[current].QuestionA;
    //         else
    //             return surveyQuestions[current].QuestionB;
    //     } else {
    //         current = -1;
    //         return null; 
    //     }   
    // }

    // public IModifyQuestion? TryGetPreviousModifyQuestion(QuestionVersion questionVersion) {
    //     if (current > 0) { 
    //         current--;
    //         if (questionVersion == QuestionVersion.QuestionA)
    //             return surveyQuestions[current].QuestionA;
    //         else
    //             return surveyQuestions[current].QuestionB;
    //     } else {
    //         current = -1;
    //         return null; 
    //     }   
    // }

    // /// <summary>
    // /// Gets question at index as ModifyQuestion.
    // /// </summary>
    // /// <param name="questionNumber"> 1-indexed question number to get. Returns null if less than 0 
    // /// or greater that question count</param>
    // public IModifyQuestion? TryGetModifyQuestion(int questionNumber, QuestionVersion questionVersion) {
    //         if (questionNumber < 1 || questionNumber > surveyQuestions.Count()) 
    //             return null;

    //         if (questionVersion == QuestionVersion.QuestionA) {
    //             return surveyQuestions[questionNumber-1].QuestionA;
    //         }
    //         else {
    //             return surveyQuestions[questionNumber-1].QuestionB;
    //         }
    // }

    // public IModifyAnswer? TryGetModifyAnswer(int answerNumber, AnswerVersion answerVersion) {
    //     if (answerNumber < 1 || answerNumber > surveyAnswers.Count()) 
    //         return null;

    //     if (answerVersion == AnswerVersion.AnswerA) {
    //         return surveyAnswers[answerNumber-1].AnswerA;
    //     }
    //     else {
    //         return surveyAnswers[answerNumber-1].AnswerB;
    //     }
    // }
    
    // void DeleteQuestionAndAnswers(int questionNumber, QuestionVersion questionVersion) {
    //     if (questionNumber > 0 || questionNumber <= surveyQuestions.Count()){

    //         surveyQuestions.RemoveAt(questionNumber-1);
    //         surveyAnswers.RemoveAt(questionNumber-1);
    //     }
    // }


    
