using System;
using System.Threading.Tasks;
using ITISKeys.Infrastructure.Context;
using ITISKeys.Infrastructure.Context.ContextFactory;
using ITISKeys.Infrastructure.Repositories;
using ITISKeys.Models;

namespace ITISKeys.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ITISKeysDbContext _context;

        public UnitOfWork(string connectionString, IITISKeysDbContextFactory context)
        {
            _context = context.CreateDbContext(connectionString);
        }

        private BaseRepository<Room> _roomRepository;

        private BaseRepository<Reservation> _reservationRepository;

        private BaseRepository<User> _userRepository;
        
        public BaseRepository<Room> RoomRepository =>
            _roomRepository ?? (_roomRepository = new BaseRepository<Room>(_context));

        public BaseRepository<Reservation> ReservationRepository =>
            _reservationRepository ?? (_reservationRepository = new BaseRepository<Reservation>(_context));

        public BaseRepository<User> UserRepository =>
            _userRepository ?? (_userRepository = new BaseRepository<User>(_context));
        
        public Task<int> Save() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
