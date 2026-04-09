namespace ExamOne.Models
{
    public class ExamModel
    {
        public string? Id { get; set; }
        public int ExamId { get; set; }
        public string? Instructions { get; set; }
        public int DurationMinutes { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime StartTime { get; set; }
        public List<ExamQuestionModel> Questions { get; set; } = new List<ExamQuestionModel>();
        public bool IsComplete { get; set; }
    }
}
