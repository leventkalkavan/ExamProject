using EntityLayer.Entities;

namespace BusinessLayer.DTOs.QuestionDtos;

public class UpdateQuestionDto
{
    public string Id { get; set; }
    public string Description { get; set; }
    public QuestionType QuestionType { get; set; }
}