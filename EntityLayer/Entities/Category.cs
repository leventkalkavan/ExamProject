using EntityLayer.Entities.Common;

namespace EntityLayer.Entities;

public class Category: BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Exam > Exams { get; set; } 
}