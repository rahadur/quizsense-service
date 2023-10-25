using Dapper;
using QuizSense.Domain.Entities;
using QuizSense.Infrastructure.DbClient;

namespace QuizSense.Infrastructure.Repositories;


public class QuizRepository : IQuizRepository
{
	private readonly IDatabasebClient dbClient;
	private readonly string TABLE = "Quizzes";

	public QuizRepository(IDatabasebClient dbClient)
	{
		this.dbClient = dbClient;
	}


	public async Task<IEnumerable<Quiz>> GetAllAsync()
	{
		return await dbClient.Get().QueryAsync<Quiz>($"SELECT * FROM {TABLE}");
	}

	public async Task<Quiz> GetByIdAsync(int id)
	{
		string sql = $"SELECT * FROM {TABLE} WHERE Id = @Id";
		return await dbClient.Get().QuerySingleAsync<Quiz>(sql, new { Id = id });
	}

	public async Task<Quiz> AddAsync(Quiz entity)
	{
		string sql = $@"INSERT INTO {TABLE}
					OUTPUT INSERTED.*
					VALUES (@UserId, @Title, @Description, @TimeLimit, @CreatedAt, @ModifiedAt)";

		return await dbClient.Get().QuerySingleAsync<Quiz>(sql, entity);
	}

	public async Task<Quiz> UpdateAsync(Quiz entity)
	{
		string sql = $@"UPDATE {TABLE}
						SET 
							Title = @Title,
							Description = @Description,
							TimeLimit = @TimeLimit,
							ModifiedAt = @ModifiedAt
						OUTPUT INSERTED.*
						WHERE Id = @Id";

		return await dbClient.Get().QuerySingleAsync<Quiz>(sql, entity);
	}

	public async Task DeleteAsync(int id)
	{
		string sql = $"DELETE FROM {TABLE} WHERE Id = @Id";
		await dbClient.Get().ExecuteAsync(sql, new { Id = id });
	}
}

public interface IQuizRepository: IRepository<Quiz>
{
}

