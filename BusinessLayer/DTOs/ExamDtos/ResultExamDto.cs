using EntityLayer.Entities;

namespace BusinessLayer.DTOs.ExamDtos;

public class ResultExamDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int ExamMinute { get; set; }
    public string Description { get; set; }
    public int SuccessScore { get; set; }
    public string CategoryName { get; set; }
}