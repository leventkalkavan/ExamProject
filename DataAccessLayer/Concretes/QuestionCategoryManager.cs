using BusinessLayer.Concretes;
using DataAccessLayer.Context;
using DataAccessLayer.Services;
using EntityLayer.Entities;

namespace DataAccessLayer.Concretes;

public class QuestionCategoryManager: BaseService<QuestionCategory>, IQuestionCategoryManager
{
    public QuestionCategoryManager(ApplicationDbContext context) : base(context)
    {
    }
}