namespace TaskManagerAPI.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        
    private DateTime _dueDate;
    public DateTime DueDate
    {
        get => _dueDate;
        set => _dueDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
        public bool IsCompleted { get; set; }
    }
}
