using EntityLayer.Entities.Common;

namespace EntityLayer.Entities;

public class Choice: BaseEntity
{
    public string Text { get; set; }
    public bool TrueChoice { get; set; }
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; }
}