namespace QuizSense.Application.Dtos;

public class QueryParameterDto
{
	public string? Search { get; set; }
	public string? OrderBy { get; set; }
	public int page { get; set; } = 1;
	public int pageSize { get; set; } = 100;
}

