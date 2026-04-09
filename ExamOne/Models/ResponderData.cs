namespace ExamOne.Models
{
    public class ResponderData<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<T> DataList { get; set; } = new List<T>();
    }
}
