namespace QuizSense.Domain.Entities;


// For multiple choice questions
public class QuizOption
{
	public long Id { get; set; }
	public long QuestionId { get; set; }
	public string Text { get; set; } = null!;
	public bool IsCorrect { get; set; }
}

