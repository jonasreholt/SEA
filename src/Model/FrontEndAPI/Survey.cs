namespace FrontEndAPI
{
    public class Survey : IGetSurvey, IModifySurvey {
        public IGetQuestion GetNextQuestion {get; private set;}

        public IGetQuestion GetQuestionA {get; private set;}
    
        public IGetQuestion GetQuestionB {get; private set;}

        public IGetAnswer GetAnswer {get; private set;}
    
        public IGetQuestion GetPreviousQuestion {get; private set;}

        public Survey() { }
    }
}