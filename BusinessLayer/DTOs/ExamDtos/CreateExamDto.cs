using EntityLayer.Entities;

namespace BusinessLayer.DTOs.ExamDtos;

public class CreateExamDto
{
    public string Name { get; set; }
    public int ExamMinute { get; set; }
    public string Description { get; set; }
    public int SuccessScore { get; set; }
    public int CategoryId { get; set; }
}