namespace ExamProjectUI.Models.ViewModels;

public class StartExamViewModel
{
    public string ExamAssignmentId { get; set; }
    public string ExamName { get; set; }
    public string ExamDescription { get; set; }
    public int ExamMinute { get; set; }
    public DateTime ExamStartTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}