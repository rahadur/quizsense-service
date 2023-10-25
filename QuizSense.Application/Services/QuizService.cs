using AutoMapper;
using QuizSense.Application.Dtos;
using QuizSense.Domain.Entities;
using QuizSense.Infrastructure.Repositories;

namespace QuizSense.Application.Services;

public class QuizService: IQuizService
{
	private readonly IQuizRepository quizRepository;
	private readonly IMapper mapper;

	public QuizService(IQuizRepository quizRepository, IMapper mapper)
	{
		this.quizRepository = quizRepository;
		this.mapper = mapper;
	}

	public IEnumerable<QuizResponse> GetAll()
	{
		var quizzes = quizRepository.GetAll();
		return mapper.Map<List<QuizResponse>>(quizzes);
	}

	public QuizResponse? GetById(int id)
	{
		var quiz = quizRepository.GetById(id);
		return mapper.Map<QuizResponse>(quiz);
	}

	public QuizResponse Add(QuizRequest body)
	{
		var quiz = mapper.Map<Quiz>(body);
		quizRepository.Add(ref quiz);
		return mapper.Map<QuizResponse>(quiz);
	}

	public QuizResponse Update(QuizRequest body)
	{
		var quiz = mapper.Map<Quiz>(body);
		quizRepository.Update(ref quiz);
		return mapper.Map<QuizResponse>(quiz);
	}

	public void Delete(int id)
	{
		quizRepository.Delete(id);
	}
}


public interface IQuizService: IService<QuizRequest, QuizResponse>
{
}