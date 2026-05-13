namespace ExamOne.Models
{
    public class DoneExamRequest
    {
        public string? sId { get; set; }
        public int estimateValue { get; set; }
    }

    public class EstimateModel
    {
        public string? CreateBy { get; set; }
        public int EstimateValue { get; set; }
    }
}
