using System.Threading.Tasks;
using ITISKeys.Infrastructure.Repositories;
using ITISKeys.Models;

namespace ITISKeys.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        BaseRepository<User> UserRepository { get; }

        BaseRepository<Room> RoomRepository { get; }

        BaseRepository<Reservation> ReservationRepository { get; }
        
        void Dispose();
        Task<int> Save();
    }
}
