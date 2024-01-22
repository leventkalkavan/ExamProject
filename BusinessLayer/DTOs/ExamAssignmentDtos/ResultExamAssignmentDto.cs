using System;

namespace BusinessLayer.DTOs.ExamAssignmentDtos
{
    public class ResultExamAssignmentDto
    {
        public string Id { get; set; }
        public string ExamName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}