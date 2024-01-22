using EntityLayer.Entities.Common;

namespace EntityLayer.Entities;

public class Exam: BaseEntity
{
    public string Name { get; set; }
    public int ExamMinute { get; set; }
    public string Description { get; set; }
    public int SuccessScore { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
    public virtual ICollection<ExamAssignment> ExamAssignments { get; set; }
}