using QuizSense.Application.Dtos;

namespace QuizSense.Application.Services;

public interface IService<TRequest, TReponse>
{
	Task<IEnumerable<TReponse>> GetAllAsync(QueryParameterDto? queryParameter);
	Task<TReponse> GetByIdAsync(int id);
	Task<TReponse> AddAsync(TRequest entity);
	Task<TReponse> UpdateAsync(TRequest entity);
	Task DeleteAsync(int id);
}

