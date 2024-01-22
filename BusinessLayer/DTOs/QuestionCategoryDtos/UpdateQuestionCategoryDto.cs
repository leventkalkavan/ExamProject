namespace BusinessLayer.DTOs.QuestionCategoryDtos;

public class UpdateQuestionCategoryDto
{
    public string Id { get; set; }
    public int ExamId { get; set; }
    public string Name { get; set; }
}