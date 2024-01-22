using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public UserRole Role { get; set; }
        public virtual ICollection<ExamAssignment> ExamAssignments { get; set; }
    }
}