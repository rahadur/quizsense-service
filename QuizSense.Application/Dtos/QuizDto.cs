namespace QuizSense.Application.Dtos;

public record QuizRequest
{
	public long?	Id			{ get; init; }
	public long		UserId		{ get; init; }
	public string	Title		{ get; init; } = null!;
	public string?	Description { get; init; }
	public int		TimeLimit	{ get; init; }
}

public class QuizResponse
{
	public long		Id			{ get; set; }
	public long?	UserId		{ get; set; }
	public string	Title		{ get; set; } = null!;
	public string?	Description { get; set; }
	public int		TimeLimit	{ get; set; }
	public DateTime CreatedAt	{ get; set; }
	public DateTime ModifiedAt	{ get; set; }
}
