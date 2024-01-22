using EntityLayer.Entities.Common;
using EntityLayer.Entities.Identity;

namespace EntityLayer.Entities;

public class ExamAnswer: BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser AppUser { get; set; }
    public int ExamAssignmentId { get; set; }
    public int QuestionId { get; set; }
    public int? Score { get; set; }
    public int? SelectedChoiceId { get; set; }
    public DateTime AnsweredAt { get; set; }
    public virtual ExamAssignment ExamAssignment { get; set; }
    public virtual Question Question { get; set; }
    public virtual Exam Exam => ExamAssignment?.Exam;
    public int ExamId { get; set; }
}