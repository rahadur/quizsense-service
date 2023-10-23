namespace QuizSense.Domain.Entities;

public class UserAnswer
{
	public long Id { get; set; }
	public long UserId { get; set; }
	public long QuestionId { get; set; }
	public long? OptionId { get; set; }
	public string? AnswerText { get; set; }
}

