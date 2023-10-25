using AutoMapper;
using QuizSense.Application.Dtos;
using QuizSense.Domain.Entities;
using QuizSense.Infrastructure.Repositories;

namespace QuizSense.Application.Services;

public class QuestionService : IQuestionService
{
	private readonly IQuestionRepository questionRepository;
	private readonly IMapper mapper;

	public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
	{
		this.questionRepository = questionRepository;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<QuestionResponse>> GetAllAsync()
	{
		var questions = await questionRepository.GetAllAsync();
		return mapper.Map<IEnumerable<QuestionResponse>>(questions);
	}

	public async Task<QuestionResponse> GetByIdAsync(int id)
	{
		var question = await questionRepository.GetByIdAsync(id);
		return mapper.Map<QuestionResponse>(question);
	}

	public async Task<QuestionResponse> AddAsync(QuestionRequest body)
	{
		var question = mapper.Map<Question>(body);
		var newQuestion = await questionRepository.AddAsync(question);
		return mapper.Map<QuestionResponse>(newQuestion);
	}

	public async Task<QuestionResponse> UpdateAsync(QuestionRequest body)
	{
		var question = mapper.Map<Question>(body);
		var updateQuestion = await questionRepository.UpdateAsync(question);
		return mapper.Map<QuestionResponse>(updateQuestion);
	}

	public async Task DeleteAsync(int id)
	{
		await questionRepository.DeleteAsync(id);
	}
	
}


public interface IQuestionService : IService<QuestionRequest, QuestionResponse>
{
}