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
    public class ReservationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("[action]")]
        public IEnumerable<Reservation> GetReservations()
        {
            return _unitOfWork.ReservationRepository.Get().Include(x => x.Reservator).Include(x => x.Room).ThenInclude(x => x.Keeper).ToList();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateReservation([FromBody] Reservation reservation)
        {
            reservation.Start = reservation.Start.AddHours(3);
            reservation.End = reservation.End.AddHours(3);

            var reservations = _unitOfWork.ReservationRepository.Get(x => x.Room.RoomNumber == reservation.Room.RoomNumber);
            
            var reservator = _unitOfWork.UserRepository.Get(x => x.Id == reservation.Reservator.Id).FirstOrDefault();

            var room = _unitOfWork.RoomRepository.Get(x => x.RoomNumber == reservation.Room.RoomNumber).Include(x=>x.Keeper).FirstOrDefault();

            reservation.Reservator = reservator;
            reservation.Room = room;


            _unitOfWork.ReservationRepository.Insert(reservation);
            
            await  _unitOfWork.Save();

            return Ok(_unitOfWork.ReservationRepository.Get().Include(x=>x.Reservator).Include(x=>x.Room).ThenInclude(x=>x.Keeper).ToList());
        }
    }
}
