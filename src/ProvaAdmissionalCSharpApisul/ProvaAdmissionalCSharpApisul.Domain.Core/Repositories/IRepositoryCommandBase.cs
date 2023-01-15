namespace ProvaAdmissionalCSharpApisul.Domain.Core.Repositories
{
    public interface IRepositoryCommandBase<TEntity> where TEntity : class
    {
		void Add(IEnumerable<TEntity> lst);
		void Add(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);
    }
}