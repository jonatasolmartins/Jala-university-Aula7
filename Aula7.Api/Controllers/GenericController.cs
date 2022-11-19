using Aula7.Api.Interfaces;
using Aula7.Api.Models;
using Aula7.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Aula7.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericController(IManagerRepository managerRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.UserRepository;
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }
        
        
        [HttpPost()]
        public IActionResult SaveUser(User user)
        {
            //User variable has no Id set yet
            _userRepository.Add(user);
            _unitOfWork.SaveChange();
            //Now user has an Id
            
            /*
             * EFCore keep tracks of any changes in the user model
             * when we save the user instance to the database it creates an incremental Id for it
             * and this instance variable was updated with this Id
             */
            return Ok(user);
        }
        
        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet()]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userRepository.GetById(id));
        }
        
        [HttpPost]
        public IActionResult SaveManager(Manager manager)
        {
            _managerRepository.Add(manager);
            return Ok();
        }
        
        [HttpGet()]
        public IActionResult GetManagerById(int id)
        {
            return Ok(_managerRepository.GetById(id));
        }
        
    }
}