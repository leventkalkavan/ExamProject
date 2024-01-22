namespace ExamProjectUI.Models.ViewModels;

public class ExamPageViewModel
{
    public string ExamAssignmentId { get; set; }
    public List<QuestionViewModel> Questions { get; set; }
    public TimeSpan RemainingTime { get; set; }
}