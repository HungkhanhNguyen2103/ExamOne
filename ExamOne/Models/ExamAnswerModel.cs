namespace ExamOne.Models
{
    public class ExamAnswerModel
    {
        public string? Id { get; set; }
        public string? ExamAnswer { get; set; }
    }

    public class AnswerModel
    {
        public int id { get; set; }
        public int index { get; set; }
        public string? answerId { get; set; }
        public bool isRead { get; set; }
    }
}
