namespace SimpleTodoApp.Model
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime? Done { get; set; }


        public TodoItem(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
        }

        public TodoItem()
        {
        }
    }
}
