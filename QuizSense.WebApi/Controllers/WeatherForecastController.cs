using Dapper;
using Microsoft.AspNetCore.Mvc;
using QuizSense.Domain.Entities;
using QuizSense.Infrastructure.DbClient;

namespace QuizSense.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IDatabasebClient _dbClient;


	public WeatherForecastController(ILogger<WeatherForecastController> logger, IDatabasebClient dbClient)
    {
        _logger = logger;
        _dbClient = dbClient;
    }

	/*
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
     */

	[HttpGet("/Quizzes")]
    public IEnumerable<Quiz> GetQuizes()
    {
        var connection = _dbClient.Get();
        var sql = "SELECT * FROM Quizzes;";
        var quizzes = connection.Query<Quiz>(sql).ToList();
        return quizzes;
	}
}

