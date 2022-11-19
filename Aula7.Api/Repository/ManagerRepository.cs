using Aula7.Api.Interfaces;
using Aula7.Api.Models;

namespace Aula7.Api.Repository
{
    public class ManagerRepository : Repository<Manager>, IManagerRepository
    {
        public ManagerRepository(EfContext context) :base(context) { }
    }
}