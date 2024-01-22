using EntityLayer.Entities.Common;

namespace EntityLayer.Entities;

public class QuestionCategory: BaseEntity
{
    public string Name { get; set; }
    public int ExamId { get; set; }
    public virtual Exam Exam { get; set; }
    public virtual ICollection<Question> Questions { get; set; }
}