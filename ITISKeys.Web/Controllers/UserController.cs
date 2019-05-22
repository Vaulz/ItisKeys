using ITISKeys.Infrastructure.UnitOfWork;
using ITISKeys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITISKeys.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUser([FromBody]User user)
        {
            var users = _unitOfWork.UserRepository.Get().ToList();
            if (users.Any(x => x.Email == user.Email))
                return Ok(_unitOfWork.UserRepository.Get(x => x.Email == user.Email).FirstOrDefault());

            _unitOfWork.UserRepository.Insert(user);

            await _unitOfWork.Save();

            return Ok(_unitOfWork.UserRepository.Get(x => x.Id == user.Id).FirstOrDefault());
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody]User newUser)
        {
            var user = _unitOfWork.UserRepository.Get(x => x.Email == newUser.Email).FirstOrDefault();
            user.Age = newUser.Age;
            user.Role = newUser.Role;
            user.Sex = newUser.Sex;
            await _unitOfWork.Save();

            return Ok(user);
        }

    }
}
