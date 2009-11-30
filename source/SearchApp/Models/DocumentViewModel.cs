namespace SearchApp.Models
{
    public class DocumentViewModel
    {
        public DocumentViewModel(string title, string text)
        {
            Title = title;
            Text = text;
        }

        public string Title { get; private set; }
        public string Text { get; private set; }
    }
}
