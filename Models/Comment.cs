namespace AuthApi.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}