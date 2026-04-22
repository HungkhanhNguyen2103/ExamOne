namespace ExamOne.Models
{
    public class ExamProgressModel
    {
        public string? Id { get; set; }
        public List<ExamQuestionModel> Questions { get; set; } = new List<ExamQuestionModel>();
        public List<AnswerModel> SelectedAnswers { get; set; } = new List<AnswerModel>();
        public int TotalCorrectAnswers { get; set; }
        public string? ComplatedDuration { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedFullName { get; set; }
        public string? BranchCode { get; set; }
        public string? BranchNameShort { get; set; }
        public string? BranchName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime MarkDate { get; set; }
        public ExamStatus ExamStatus { get; set; }
    }

    public enum ExamStatus
    {
        NotYet,
        InProgress,
        Done,
        Error
    }
}
