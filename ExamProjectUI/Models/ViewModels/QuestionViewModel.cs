using EntityLayer.Entities;

namespace ExamProjectUI.Models.ViewModels;

public class QuestionViewModel
{
    public int QuestionId { get; set; }
    public string Description { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<ChoiceViewModel> Choices { get; set; }

}