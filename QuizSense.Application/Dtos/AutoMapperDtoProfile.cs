using AutoMapper;
using QuizSense.Domain.Commom;
using QuizSense.Domain.Entities;

namespace QuizSense.Application.Dtos;

public class AutoMapperDtoProfile: Profile
{
	public AutoMapperDtoProfile()
	{
		CreateMap<QueryParameterDto, QueryParameter>().ReverseMap();

		CreateMap<QuizRequest, Quiz>();
		CreateMap<Quiz, QuizResponse>();
		CreateMap<QuestionRequest, Question>();
		CreateMap<Question, QuestionResponse>();
	}
}

