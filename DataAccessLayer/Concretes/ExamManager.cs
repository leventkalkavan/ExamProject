using BusinessLayer.Concretes;
using DataAccessLayer.Context;
using DataAccessLayer.Services;
using EntityLayer.Entities;

namespace DataAccessLayer.Concretes;

public class ExamManager: BaseService<Exam>, IExamManager
{
    public ExamManager(ApplicationDbContext context) : base(context)
    {
    }
}