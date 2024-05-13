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
    private int surveyId;
    private List<Question> surveyQuestions = new List<Question>();
    
    public Survey(int surveyId) { 
        this.surveyId = surveyId;
    }

    public IReadOnlyQuestion TryGetNextReadOnlyQuestion() {
        if(current < (surveyQuestions.Count() - 1)) { 
            current ++;
            return surveyQuestions[current];
        } else{
            current = surveyQuestions.Count();
            return null;
            
    }

    public IModifyQuestion TryGetNextModifyQuestion() {
        if(current < (surveyQuestions.Count() - 1)) { 
            current ++;
            return surveyQuestions[current];
        } else{
            current = surveyQuestions.Count();
            return null;
        }
    }

    public IReadOnlyQuestion TryGetPreviousReadOnlyQuestion() {
        if (current > 0) { 
            current --;
            return surveyQuestions[current];
        } else {
            current = 0;
            return null; 
        }   
    }

    public IModifyQuestion TryGetPreviousModifyQuestion() {
        if (current > 0) { 
            current --;
            return surveyQuestions[current];
        } 
        else {
            current = -1;
            return null;    
        }
    }
    
    public IModifyQuestion CreateQuestion(){
        
    }

}