using Dapper;
using QuizSense.Domain.Entities;
using QuizSense.Infrastructure.DbClient;

namespace QuizSense.Infrastructure.Repositories;

public class QuestionRepository : IQuestionRepository
{
	private readonly IDatabasebClient dbClient;
	private readonly string TABLE = "Questions";

	public QuestionRepository(IDatabasebClient dbClient)
	{
		this.dbClient = dbClient;
	}

	public async Task<IEnumerable<Question>> GetAllAsync()
	{
		return await dbClient.Get().QueryAsync<Question>($"SELECT * FROM {TABLE}");
	}

	public async Task<Question?> GetByIdAsync(int id)
	{
		string sql = $"SELECT * FROM {TABLE} WHERE Id = @Id";
		return await dbClient.Get().QuerySingleOrDefaultAsync<Question>(sql, new { Id = id });
	}

	public async Task<Question> AddAsync(Question entity)
	{
		string sql = $@"INSERT INTO {TABLE}
						OUTPUT INSERTED.*
						VALUES(@QuizId, @Text, @Type, @Point)";

		return await dbClient.Get().QuerySingleAsync<Question>(sql, entity);
	}

	public async Task<Question> UpdateAsync(Question entity)
	{
		string sql = $@"UPDATE {TABLE}
						SET
							QuizId = @QuizId,
							Text   = @Text,
							Type   = @Type,
							Point  = @Point
						OUTPUT INSERTED.*
						WHERE Id = @Id";

		return await dbClient.Get().QuerySingleAsync<Question>(sql, entity);
	}

	public async Task DeleteAsync(int id)
	{
		string sql = $"DELETE FROM {TABLE} WHERE Id = @Id";
		await dbClient.Get().ExecuteAsync(sql, new { Id = id });
	}
}

public interface IQuestionRepository : IRepository<Question>
{
}

