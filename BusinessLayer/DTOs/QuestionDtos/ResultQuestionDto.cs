using BusinessLayer.DTOs.ChoiceDto;
using EntityLayer.Entities;

namespace BusinessLayer.DTOs.QuestionDtos
{
    public class ResultQuestionDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionCategoryName { get; set; }
        public List<ResultChoiceDto> Choices { get; set; }
    }
}