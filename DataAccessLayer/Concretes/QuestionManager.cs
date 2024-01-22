using BusinessLayer.Concretes;
using DataAccessLayer.Context;
using DataAccessLayer.Services;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concretes;

public class QuestionManager: BaseService<Question>, IQuestionManager
{
    public QuestionManager(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Question> GetQuestionsForExam(int examId, bool tracking = true)
    {
        if (tracking)
        {
            return GetAll()
                .Include(q => q.Choices)
                .Where(q => q.Id == examId);
        }
        else
        {
            return GetAll(tracking: false)
                .Include(q => q.Choices)
                .Where(q => q.Id == examId);
        }
    }
}