using System.Text;
using Dapper;
using QuizSense.Domain.Commom;
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

	public async Task<IEnumerable<Question>> GetAllAsync(QueryParameter? queryParameter)
	{
		var sqlBuilder = new StringBuilder($"SELECT * FROM {TABLE}");
		var parameter = new DynamicParameters();

		BuildWhereClause(queryParameter, ref sqlBuilder, ref parameter);

		return await dbClient.Get().QueryAsync<Question>(sqlBuilder.ToString(), parameter);
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




	private void BuildWhereClause(QueryParameter? queryParameter, ref StringBuilder sqlBuilder, ref DynamicParameters parameter)
	{
		if (queryParameter != null)
		{
			sqlBuilder.Append(" WHERE 1 = 1");
			if (!string.IsNullOrEmpty(queryParameter.Search))
			{
				sqlBuilder.Append(" AND Text LIKE @Search");
				parameter.Add("Search", $"%{queryParameter.Search}%");
			}

			if (!string.IsNullOrEmpty(queryParameter.OrderBy))
			{
				var orderBy = queryParameter.OrderBy.Split(' ');
				if (orderBy.Length == 2)
				{
					sqlBuilder.Append($" ORDER BY {orderBy[0]} {orderBy[1]}");
				}
			}
			else
			{
				sqlBuilder.Append($" ORDER BY Id ASC");
			}

			int page = queryParameter.page > 1 ? (queryParameter.page) - 1 : 0;
			sqlBuilder.Append($" OFFSET @OffestRows ROWS FETCH NEXT @PageSize ROWS ONLY");
			parameter.Add("OffestRows", (queryParameter.page - 1) * queryParameter.pageSize);
			parameter.Add("PageSize", queryParameter.pageSize);

		}
	}

	public Task<IEnumerable<Question>> QuizQuestionsAsync(int quizId)
	{
		string sql = $"SELECT * FROM {TABLE} WHERE QuizId = @QuizId";
		return dbClient.Get().QueryAsync<Question>(sql, new { QuizId = quizId });
	}
}

public interface IQuestionRepository : IRepository<Question>
{
	Task<IEnumerable<Question>> QuizQuestionsAsync(int quizId);
}

