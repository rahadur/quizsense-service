namespace QuizSense.Domain.Entities;

public class Quiz
{
	public long Id { get; set; }
	public long? UserId { get; set; }
	public string Title { get; set; } = null!;
	public string Description { get; set; } = string.Empty;
	public int TimeLimit { get; set; }
	public DateTime CreationAt { get; set; }
	public DateTime ModifiedAt { get; set; }
}

