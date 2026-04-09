namespace ExamOne.Entity
{
    public class QuestionBank
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Question { get; set; }
        public string? Items { get; set; }
        public string? Explanation { get; set; }
        public string? Article { get; set; }
        public string? Type { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreatedBy { get; set; }

    }
}
