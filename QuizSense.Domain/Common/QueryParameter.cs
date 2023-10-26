namespace QuizSense.Domain.Commom;

public class QueryParameter
{
	public string? Search { get; set; }
	public string? OrderBy { get; set; }
	public int page { get; set; } = 1;
	public int pageSize { get; set; } = 100;
}

