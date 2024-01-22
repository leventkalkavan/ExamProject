using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.ExamAssignmentDtos
{
    public class CreateExamAssignmentDto
    {
        public int ExamId { get; set; }
        public Guid UserId { get; set; }
        [DataType(DataType.DateTime)] public DateTime StartTime { get; set; }
        [DataType(DataType.DateTime)] public DateTime EndTime { get; set; }
    }
}