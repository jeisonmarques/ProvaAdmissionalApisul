using System.Collections.Generic;

namespace ProvaAdmissionalCSharpApisul.Domain.Core.Repositories
{
    public interface IRepositoryQueryBase<TEntity> where TEntity : class
    {
        TEntity ById(int id);
        IEnumerable<TEntity> List();
    }
}