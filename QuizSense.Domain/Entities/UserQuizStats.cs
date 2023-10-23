namespace QuizSense.Domain.Entities;

public class UserQuizStats
{
	public long Id { get; set; }
	public long UserId { get; set; }
	public long QuizId { get; set; }
	public DateTime TakenDate { get; set; }
	public double Score { get; set; }
}

