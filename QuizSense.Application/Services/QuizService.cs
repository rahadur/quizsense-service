using AutoMapper;
using QuizSense.Application.Dtos;
using QuizSense.Domain.Commom;
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

	public async Task<IEnumerable<QuizResponse>> GetAllAsync(QueryParameterDto? queryParameter)
	{
		var param = mapper.Map<QueryParameter>(queryParameter);
		var quizzes = await quizRepository.GetAllAsync(param);
		return mapper.Map<List<QuizResponse>>(quizzes);
	}

	public async Task<QuizResponse> GetByIdAsync(int id)
	{
		var quiz = await quizRepository.GetByIdAsync(id);
		return mapper.Map<QuizResponse>(quiz);
	}

	public async Task<QuizResponse> AddAsync(QuizRequest body)
	{
		var quiz = mapper.Map<Quiz>(body);
		var newQuiz = await quizRepository.AddAsync(quiz);
		return mapper.Map<QuizResponse>(newQuiz);
	}

	public async Task<QuizResponse> UpdateAsync(QuizRequest body)
	{
		var quiz = mapper.Map<Quiz>(body);
		var updatedQuiz = await quizRepository.UpdateAsync(quiz);
		return mapper.Map<QuizResponse>(updatedQuiz);
	}

	public async Task DeleteAsync(int id)
	{
		await quizRepository.DeleteAsync(id);
	}
}


public interface IQuizService: IService<QuizRequest, QuizResponse>
{
}