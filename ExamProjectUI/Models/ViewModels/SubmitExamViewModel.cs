namespace ExamProjectUI.Models.ViewModels;

public class SubmitExamViewModel
{
    public int ExamAssignmentId { get; set; }
    public List<QuestionAnswerViewModel> QuestionAnswers { get; set; }
}