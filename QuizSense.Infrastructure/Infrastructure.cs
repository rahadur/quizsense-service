using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizSense.Infrastructure.DbClient;

namespace QuizSense.Infrastructure;

public static class Infrastructure
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped((Func<IServiceProvider, IDatabasebClient>)(provider => new DbClient.DatabaseClient(configuration.GetConnectionString("SqlConnection"))));
		return services;
	}
}

