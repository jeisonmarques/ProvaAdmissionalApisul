namespace ProvaAdmissionalCSharpApisul.Domain.Core.Repositories
{
    public interface IUnitOfWork<TContext> where TContext : class
    {
        void Commit();
    }
}