namespace SagaPattern.Models
{
    public class BookingSaga
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCancelled { get; set; }
        public List<string> Steps { get; set; }

        public BookingSaga()
        {
            Steps = new List<string>();
        }
    }

}
