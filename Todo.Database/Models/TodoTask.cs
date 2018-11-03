namespace Todo.Database.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
