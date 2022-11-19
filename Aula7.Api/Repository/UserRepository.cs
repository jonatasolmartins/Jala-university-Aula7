using Aula7.Api.Interfaces;
using Aula7.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Aula7.Api.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly EfContext _context;
        public UserRepository(EfContext context) :base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        
        public void Add(User instance)
        {
            _context.Users.Add(instance);
        }

        public User GetById(int id)
        {
            return _context.Users.AsTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }
        
        
        
    }
}