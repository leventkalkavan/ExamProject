using BusinessLayer.Concretes;
using DataAccessLayer.Context;
using DataAccessLayer.Services;
using EntityLayer.Entities;

namespace DataAccessLayer.Concretes;

public class ExamAssignmentManager: BaseService<ExamAssignment>, IExamAssignmentManager
{
    public ExamAssignmentManager(ApplicationDbContext context) : base(context)
    {
    }
}