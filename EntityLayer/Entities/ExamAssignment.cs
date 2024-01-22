using EntityLayer.Entities.Common;
using EntityLayer.Entities.Identity;

namespace EntityLayer.Entities
{
    public class ExamAssignment : BaseEntity
    {
        public Guid UserId { get; set; }
        public int ExamId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Exam Exam { get; set; }

        public virtual ICollection<ExamAnswer> UserExamAnswers { get; set; }
    }
}