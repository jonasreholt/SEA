namespace Question;

public class QuestionPair {
    public Question? QuestionA {get; private set;}
    
    public Question? QuestionB { get; private set;}

    public QuestionPair(Question questionA, Question questionB)
    {
        QuestionA = questionA;
        QuestionB = questionB;
    }
}