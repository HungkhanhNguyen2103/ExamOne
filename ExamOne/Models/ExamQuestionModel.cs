namespace ExamOne.Models
{
    public class ExamQuestionModel
    {
        public int Id { get; set; }
        public int QuestionIndex { get; set; }
        public string? Description { get; set; }
        public string? Question { get; set; }
        public string? Options { get; set; }    
        public List<OptionModel> Items { get; set; } = new List<OptionModel>();
        public string? Explanation { get; set; }
        public string? Article { get; set; }
        public int AnsweredOptionIndex { get; set; }
        public string? AnsweredOptionCharacter { get; set; }
        public string? AnsweredOptionId { get; set; }
        public string? UserAnsweredOptionCharacter { get; set; }
        public string? UserAnsweredOptionId { get; set; }
        public int Point { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateDateStr { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class OptionModel
    {
        public string? Id { get; set; }
        public int SortOrder { get; set; }
        public string? OptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
