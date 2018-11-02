namespace Todo.Server.Models
{
    public class TodoTaskModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
