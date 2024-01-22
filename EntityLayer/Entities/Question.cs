using EntityLayer.Entities.Common;

namespace EntityLayer.Entities;

public class Question: BaseEntity
{
    public string Description { get; set; }
    public int QuestionCategoryId { get; set; }
    public QuestionType QuestionType { get; set; }
    public virtual QuestionCategory QuestionCategory { get; set; }
    public virtual ICollection<Choice> Choices { get; set; }
}