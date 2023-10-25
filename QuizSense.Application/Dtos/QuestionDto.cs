using QuizSense.Domain.Enums;

namespace QuizSense.Application.Dtos;

public record QuestionRequest
{
	public long?		Id		{ get; init; }
	public long			QuizId	{ get; init; }
	public string		Text	{ get; init; } = null!;
	public QuestionType Type	{ get; init; }
	public double		Point	{ get; init; }
}


public class QuestionResponse
{
	public long			Id		{ get; set; }
	public long			QuizId	{ get; set; }
	public string		Text	{ get; set; } = null!;
	public QuestionType Type	{ get; set; }
	public double		Point	{ get; set; }
}

