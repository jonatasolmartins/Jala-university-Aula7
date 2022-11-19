using Aula7.Api.Interfaces;

namespace Aula7.Api.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly EfContext _context;
    private IUserRepository _userRepository;

    public UnitOfWork(EfContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _userRepository ??= new UserRepository(_context);
        }
    }

    public void SaveChange()
    {
        _context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;
        
        ResetRepositories();
        _context.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private void ResetRepositories()
    {
        _userRepository = null;
    }
}