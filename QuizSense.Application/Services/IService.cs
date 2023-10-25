namespace QuizSense.Application.Services;

public interface IService<TRequest, TReponse>
{
	IEnumerable<TReponse> GetAll();
	TReponse? GetById(int id);
	TReponse Add(TRequest entity);
	TReponse Update(TRequest entity);
	void Delete(int id);
}

