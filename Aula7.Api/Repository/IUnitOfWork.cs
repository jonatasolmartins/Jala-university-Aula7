using Aula7.Api.Interfaces;

namespace Aula7.Api.Repository;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }
    public void SaveChange();
}