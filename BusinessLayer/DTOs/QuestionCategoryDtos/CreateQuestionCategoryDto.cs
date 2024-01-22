using System.Collections;

namespace BusinessLayer.DTOs.QuestionCategoryDtos;

public class CreateQuestionCategoryDto
{
    public string Name { get; set; }
    public int ExamId { get; set; }
}