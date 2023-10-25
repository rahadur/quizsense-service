using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizSense.Infrastructure.DbClient;
using QuizSense.Infrastructure.Repositories;

namespace QuizSense.Infrastructure;

public static class Infrastructure
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped((Func<IServiceProvider, IDatabasebClient>)(provider => new DatabaseClient(configuration.GetConnectionString("SqlConnection"))));

		services.AddScoped<IQuizRepository, QuizRepository>();

		return services;
	}
}

