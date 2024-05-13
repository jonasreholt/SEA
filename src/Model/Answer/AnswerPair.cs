namespace Answer;

public class AnswerPair {
    public Answer? AnswerA {get; private set;}
    
    public Answer? AnswerB { get; private set;}

    public AnswerPair(Answer answerA, Answer answerB)
    {
        AnswerA = answerA;
        AnswerB = answerB;
    }
}