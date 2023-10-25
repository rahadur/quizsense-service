namespace QuizSense.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
	IEnumerable<TEntity> GetAll();
	TEntity? GetById(int id);
	void Add(ref TEntity entity);
	void Update(ref TEntity entity);
	void Delete(int id);
}

