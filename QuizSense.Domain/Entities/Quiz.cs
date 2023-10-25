namespace QuizSense.Domain.Entities;

public class Quiz
{
	public long		Id			{ get; set; }
	public long?	UserId		{ get; set; }
	public string	Title		{ get; set; } = null!;
	public string	Description { get; set; } = string.Empty;
	public int		TimeLimit	{ get; set; }
	public DateTime CreatedAt	{ get; set; } = DateTime.Now;
	public DateTime ModifiedAt	{ get; set; } = DateTime.Now;
}

