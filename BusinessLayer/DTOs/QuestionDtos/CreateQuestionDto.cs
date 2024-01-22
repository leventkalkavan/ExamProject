using EntityLayer.Entities;
using System.Collections.Generic;
using BusinessLayer.DTOs.ChoiceDto;

namespace BusinessLayer.DTOs.QuestionDtos
{
    public class CreateQuestionDto
    {
        public int QuestionCategoryId { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Description { get; set; }
        public List<CreateChoiceDto> Choices { get; set; }
    }
}