namespace ExamOne.Models
{
    public class RankingModel
    {
        public int Rank { get; set; }
        public string? UserName { get; set; }
        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public string? BranchName { get; set; }
        public int CorrectAnswer { get; set; }
        public string? CompletionTime { get; set; }
        public double ComplatedDuration { get; set; }
        public int TryExamCount { get; set; }
        public int TryExamErrorCount { get; set; }
        public int TryExamSuccessCount { get; set; }
        public int TryExamPendingCount { get; set; }
        public int EstimatePersonValue { get; set; }
    }

    public class RankingBranchModel
    {
        public int Rank { get; set; }
        public string? BranchCode { get; set; }
        public string? BranchName { get; set; }
        public string? BranchNameFull { get; set; }
        public double AverageScore { get; set; }    
        public double AverageComplatedDuration { get; set; }    
        public double AverageCorrectAnswer { get; set; }
        public string? AverageCorrectAnswerCeil { get; set; }
        public string? AverageCompletionTime { get; set; }

        public List<RankingModel> Users { get; set; } = new List<RankingModel>();
    }
}
