using ITISKeys.Infrastructure.UnitOfWork;
using ITISKeys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITISKeys.Web.Filters;

namespace ITISKeys.Web.Controllers
{
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("[action]")]
        [CheckActionFilter]
        public IActionResult GetRooms()
        {
            try
            {
                var rooms = _unitOfWork.RoomRepository.Get().Include(x => x.Keeper).ToList();
                return Ok(rooms);
            }
            catch (Exception e)
            {
                HttpContext.Response.Cookies.Append("Error", DateTime.Now.ToString("dd/MM/yyyy hh-mm-ss"));
                return BadRequest();
                throw e;
            }
        }

        [HttpPost("[action]")]
        [CheckActionFilter]
        public async Task<IActionResult> CreateRoom([FromBody] Room room)
        {
            var rooms = _unitOfWork.RoomRepository.Get().ToList();
            if (rooms.Any(x => x.RoomNumber==room.RoomNumber))
                return Ok(_unitOfWork.RoomRepository.Get().Include(x => x.Keeper).ToList());


            _unitOfWork.RoomRepository.Insert(room);
            await _unitOfWork.Save();
            return Ok(_unitOfWork.RoomRepository.Get().Include(x => x.Keeper).ToList());
        }

        [HttpPut("[action]")]
        [CheckActionFilter]
        public async Task<IActionResult> TakeKey([FromBody] Room newRoom)
        {
            var room = _unitOfWork.RoomRepository.Get(x => x.Id == newRoom.Id).FirstOrDefault();
            room.InStock = newRoom.InStock;
            var keeper = _unitOfWork.UserRepository.Get(x => x.Email == newRoom.Keeper.Email).FirstOrDefault();
            room.Keeper = keeper;
            
            await  _unitOfWork.Save();

            return Ok(_unitOfWork.RoomRepository.Get().Include(x=>x.Keeper).ToList());
        }

        [HttpPut("[action]")]
        [CheckActionFilter]
        public async Task<IActionResult> ReturnKey([FromBody] Room newRoom)
        {
            var room = _unitOfWork.RoomRepository.Get(x => x.Id == newRoom.Id).FirstOrDefault();

            room.InStock = newRoom.InStock;

            _unitOfWork.RoomRepository.SetNull(room, "KeeperId");

            await _unitOfWork.Save();

            return Ok(_unitOfWork.RoomRepository.Get().Include(x => x.Keeper).ToList());
        }
    }
}
