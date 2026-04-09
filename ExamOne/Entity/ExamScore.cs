namespace ExamOne.Entity
{
    public class ExamScore
    {
        public int Id { get; set; }
        public int CorrectAnswer { get; set; }
        public int TotalAnswer { get; set; }
        public string? CreateBy { get; set; }
        public string? ExamQuestionId { get; set; }
    }
}
