using AutoMapper;
using QuizSense.Domain.Entities;

namespace QuizSense.Application.Dtos;

public class AutoMapperDtoProfile: Profile
{
	public AutoMapperDtoProfile()
	{
		CreateMap<QuizRequest, Quiz>();
		CreateMap<Quiz, QuizResponse>();
		CreateMap<QuestionRequest, Question>();
		CreateMap<Question, QuestionResponse>();
	}
}

