using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizSense.Infrastructure;
using QuizSense.Application.Dtos;
using QuizSense.Application.Services;


namespace QuizSense.Application;

public static class Application
{
	public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddInfrastructure(configuration);
		services.AddAutoMapper(typeof(AutoMapperDtoProfile));
		// Add Application Services
		services.AddScoped<IQuizService, QuizService>();

		return services;
	}
}

