namespace QuizSense.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
	Task<IEnumerable<TEntity>> GetAllAsync();
	Task<TEntity?> GetByIdAsync(int id);
	Task<TEntity> AddAsync(TEntity entity);
	Task<TEntity> UpdateAsync(TEntity entity);
	Task DeleteAsync(int id);
}

