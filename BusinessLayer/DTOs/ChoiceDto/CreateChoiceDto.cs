namespace BusinessLayer.DTOs.ChoiceDto;

public class CreateChoiceDto
{
    public string Text { get; set; }
    public int QuestionId { get; set; }
    public bool TrueChoice { get; set; }
}