using System;
namespace Survey;

using Question;
using Answer;


public class Survey : IReadOnlySurvey, IModifySurvey {
    public IReadOnlyQuestion ReadOnlyQuestionA {get;}
    
    public IReadOnlyQuestion ReadOnlyQuestionB {get;}

    public IReadOnlyAnswer ReadOnlyAnswer {get;}
    

    //Go to previous question, when modifying questions.
    // public IModifyQuestion SetGoToPreviousQuestion {get; set;}    
    public IModifyQuestion ModifyQuestionA {get; set;}
    public IModifyQuestion ModifyQuestionB {get; set;}
    public IModifyAnswer ModifyAnswer {get; set;}   
    private int current;    
    public int surveyId {get;}

    private List<QuestionPair?> surveyQuestions = new List<QuestionPair>();

    private List<AnswerPair?> surveyAnswers = new List<AnswerPair>();

    public Survey(int surveyId) { 
        this.surveyId = surveyId;
    }

    /// <summary>
    /// Tries to get version A or B of a question pair. Returns null if the last
    /// question has been reached.
    /// </summary>
    /// <param name="questionAorB">question A="QuestionA" and question B="QuestionB"</param>
    public IReadOnlyQuestion? TryGetNextReadOnlyQuestion(QuestionVersion questionVersion) {
        if(current < (surveyQuestions.Count() - 1)) { 
            current++;
            if (questionVersion == QuestionVersion.QuestionA)
                return surveyQuestions[current].QuestionA;
            else
                return surveyQuestions[current].QuestionB;
        } else{
            current = surveyQuestions.Count();
            return null;
        }  
    }

    public IModifyQuestion? TryGetNextModifyQuestion(QuestionVersion questionVersion) {
        if(current < (surveyQuestions.Count() - 1)) { 
            current++;
            if (questionVersion == QuestionVersion.QuestionA)
                return surveyQuestions[current].QuestionA;
            else
                return surveyQuestions[current].QuestionB;
        } else{
            current = surveyQuestions.Count();
            return null;
        }  
    }

    public IReadOnlyQuestion? TryGetPreviousReadOnlyQuestion(QuestionVersion questionVersion) {
        if (current > 0) { 
            current--;
            if (questionVersion == QuestionVersion.QuestionA)
                return surveyQuestions[current].QuestionA;
            else
                return surveyQuestions[current].QuestionB;
        } else {
            current = -1;
            return null; 
        }   
    }

    public IModifyQuestion? TryGetPreviousModifyQuestion(QuestionVersion questionVersion) {
        if (current > 0) { 
            current--;
            if (questionVersion == QuestionVersion.QuestionA)
                return surveyQuestions[current].QuestionA;
            else
                return surveyQuestions[current].QuestionB;
        } else {
            current = -1;
            return null; 
        }   
    }

    /// <summary>
    /// Gets question at index as ModifyQuestion.
    /// </summary>
    /// <param name="questionNumber"> 1-indexed question number to get. Returns null if less than 0 
    /// or greater that question count</param>
    public IModifyQuestion? TryGetModifyQuestion(int questionNumber, QuestionVersion questionVersion) {
            if (questionNumber < 1 || questionNumber > surveyQuestions.Count()) 
                return null;

            if (questionVersion == QuestionVersion.QuestionA) {
                return surveyQuestions[questionNumber-1].QuestionA;
            }
            else {
                return surveyQuestions[questionNumber-1].QuestionB;
            }
    }

    public IModifyAnswer? TryGetModifyAnswer(int answerNumber, AnswerVersion answerVersion) {
        if (answerNumber < 1 || answerNumber > surveyAnswers.Count()) 
            return null;

        if (answerVersion == AnswerVersion.AnswerA) {
            return surveyAnswers[answerNumber-1].AnswerA;
        }
        else {
            return surveyAnswers[answerNumber-1].AnswerB;
        }
    }
    
    void DeleteQuestionAndAnswers(int questionNumber, QuestionVersion questionVersion) {
        if (questionNumber > 0 || questionNumber <= surveyQuestions.Count()){
            // if (questionVersion == QuestionVersion.QuestionA) {
            //     surveyQuestions[questionNumber-1].QuestionA = null;
            //     surveyAnswers[questionNumber-1].AnswerA = null;
            // } else {
            //     surveyQuestions[questionNumber-1].QuestionB = null;
            //     surveyAnswers[questionNumber-1].AnswerB = null;
            // }


            // If both question versions should be deleted.
            surveyQuestions.RemoveAt(questionNumber-1);
            surveyAnswers.RemoveAt(questionNumber-1);
        }
    }

    // public void CreateNewQuestion(QuestionPair questionPair) {
    //     var question = new Question(Guid.NewGuid());
    // }

    

}