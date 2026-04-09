namespace ExamOne.Entity
{
    public class Exam
    {
        public int Id { get; set; }
        public string? Instructions { get; set; }
        public int DurationMinutes { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
