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
        public bool IsEstimated { get; set; }
    }

    public class ExamDisplayModel
    {
        public string ID { get; set; }
        public int Index { get; set; }
        public string CreateDate { get; set; }
        public int CorrectAnswer { get; set; }
        public int TotalAnswer { get; set; }
        public string Duration { get; set; }
    }
}
