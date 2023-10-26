using System.Text;
using Dapper;
using QuizSense.Domain.Commom;
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

	public async Task<IEnumerable<Quiz>> GetAllAsync(QueryParameter? queryParameter)
	{
		var sqlBuilder = new StringBuilder($"SELECT * FROM {TABLE} ");
		var parameter = new DynamicParameters();
		BuildWhereClause(queryParameter, ref sqlBuilder, ref parameter);
		return await dbClient.Get().QueryAsync<Quiz>(sqlBuilder.ToString(), parameter);
	}

	public async Task<Quiz?> GetByIdAsync(int id)
	{
		string sql = $"SELECT * FROM {TABLE} WHERE Id = @Id";
		return await dbClient.Get().QuerySingleOrDefaultAsync<Quiz>(sql, new { Id = id });
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
		string sql = $" DELETE FROM {TABLE} WHERE Id = @Id";
		await dbClient.Get().ExecuteAsync(sql, new { Id = id });
	}

	private void BuildWhereClause(QueryParameter? queryParameter, ref StringBuilder sqlBuilder, ref DynamicParameters parameter)
	{
		if (queryParameter != null)
		{
			sqlBuilder.Append("WHERE 1 = 1 ");
			if (!string.IsNullOrEmpty(queryParameter.Search))
			{
				sqlBuilder.Append("AND Title LIKE @Search ");
				sqlBuilder.Append("OR Description LIKE @Search ");
				parameter.Add("Search", $"%{queryParameter.Search}%");
			}

			if (string.IsNullOrEmpty(queryParameter.OrderBy))
			{
				sqlBuilder.Append($"ORDER BY Id ASC ");
			}
			else
			{
				var orderBy = queryParameter.OrderBy.Split(' ');
				if (orderBy.Length == 2)
				{
					sqlBuilder.Append($"ORDER BY {orderBy[0]} {orderBy[1]} ");
				}
				
			}

			int page = queryParameter.page > 1 ? (queryParameter.page) - 1 : 0;
			sqlBuilder.Append("OFFSET @OffsetRows ROWS FETCH NEXT @PageRows ROWS ONLY ");
			parameter.Add("OffsetRows", page * queryParameter.pageSize);
			parameter.Add("PageRows", queryParameter.pageSize);
		}
	}
}



public interface IQuizRepository: IRepository<Quiz>
{
}

