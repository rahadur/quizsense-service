using Microsoft.Data.SqlClient;

namespace QuizSense.Infrastructure.DbClient;

public class DatabaseClient: Disposable, IDatabasebClient
{
	private SqlConnection? connection;
	private readonly string _connectionString;

	public DatabaseClient(string connectionString)
	{
		_connectionString = connectionString;
	}

	public SqlConnection Get()
	{
		Console.WriteLine(_connectionString);
		return connection ?? (connection = new SqlConnection(_connectionString));
	}

	protected override void DisposeClient()
	{
		if (connection != null)
		{
			connection.Dispose();
		}
	}

}

