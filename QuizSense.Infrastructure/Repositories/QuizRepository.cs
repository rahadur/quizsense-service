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

	public IEnumerable<Quiz> GetAll()
	{
		return dbClient.Get().Query<Quiz>($"SELECT * FROM {TABLE}");
	}

	public Quiz? GetById(int id)
	{
		string sql = $"SELECT * FROM {TABLE} WHERE Id = @Id";
		return dbClient.Get().QuerySingleOrDefault<Quiz>(sql, new { Id = id });
	}

	public void Add(ref Quiz entity)
	{
		string sql = $@"INSERT INTO {TABLE}
					OUTPUT INSERTED.*
					VALUES (@UserId, @Title, @Description, @TimeLimit, @CreatedAt, @ModifiedAt)";

		entity = dbClient.Get().QuerySingle<Quiz>(sql, entity);
	}


	public void Update(ref Quiz entity)
	{
		string sql = $@"UPDATE {TABLE}
						SET 
							Title = @Title,
							Description = @Description,
							TimeLimit = @TimeLimit,
							ModifiedAt = @ModifiedAt
						OUTPUT INSERTED.*
						WHERE Id = @Id";

		entity = dbClient.Get().QuerySingle<Quiz>(sql, entity);
	}

	public void Delete(int id)
	{
		string sql = $"DELETE FROM {TABLE} WHERE Id = @Id";
		dbClient.Get().Execute(sql, new { Id = id });
	}

}

public interface IQuizRepository: IRepository<Quiz>
{
}

