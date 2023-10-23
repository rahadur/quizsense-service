using QuizSense.Domain.Enums;

namespace QuizSense.Domain.Entities;

public class Question
{
	public long Id { get; set; }
	public long QuizId { get; set; }
	public string Text { get; set; } = null!;
	public QuestionType Type { get; set; } = QuestionType.MultipleChoice;
	public double Point { get; set; }
}

