using BusinessLayer.Abstract;
using EntityLayer.Entities;

namespace BusinessLayer.Concretes;

public interface IQuestionManager: IBaseService<Question>
{
    IQueryable<Question> GetQuestionsForExam(int examId, bool tracking = true);
}