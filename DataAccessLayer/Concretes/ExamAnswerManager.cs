using BusinessLayer.Concretes;
using DataAccessLayer.Context;
using DataAccessLayer.Services;
using EntityLayer.Entities;

namespace DataAccessLayer.Concretes;

public class ExamAnswerManager: BaseService<ExamAnswer>, IExamAnswerManager
{
    public ExamAnswerManager(ApplicationDbContext context) : base(context)
    {
    }
}