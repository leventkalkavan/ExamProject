namespace BusinessLayer.DTOs.ChoiceDto;

public class UpdateChoiceDto
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; }
    public bool TrueChoice { get; set; }
}