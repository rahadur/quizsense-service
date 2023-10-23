using Microsoft.Data.SqlClient;

namespace QuizSense.Infrastructure.DbClient;

public interface IDatabasebClient: IDisposable
{
	SqlConnection Get();
}

