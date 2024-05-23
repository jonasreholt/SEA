using Model.Answer;

namespace Tests.Frontend.Mocks;

using Model.Question;
using Answer = Model.Answer.Answer;

internal class ReadOnlyQuestionMock : IReadOnlyQuestion
{

    public int QuestionId {get; set; }
    public string ReadOnlyCaption {get; set; }
    public string ReadOnlyPicture {get; set; }
    public string ReadOnlyText{get; set; }
    public IReadOnlyAnswer ReadOnlyAnswer {get; set; }
}
