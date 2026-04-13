namespace ExamOne.Models
{
    public class ProfileModel
    {
        public string? UserName { get; set; }
        public string? Avatar { get; set; }
        public ProfileStatus ExamStatus { get; set; }
        public int TotalCorrectAnswer { get; set; }
        public int TotalQuestions { get; set; }
        public string? DurationTime { get; set; }
        public string? Location { get; set; }
        public string? CCCD { get; set; }
        public string? FullName { get; set; }
        public int Rank { get; set; }
        public int EstimateCount { get; set; }
        public string? EstimateDisplay { get; set; }
        public List<ExamDisplayModel> ExamDisplays { get; set; } = new List<ExamDisplayModel>();
    }


    public enum ProfileStatus
    {
        NotYet = 0,
        Done = 1,
        Marked = 2,
    }
}
