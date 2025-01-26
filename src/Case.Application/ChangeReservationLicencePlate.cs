using Case.Application.Exceptions;
using Case.Domain.Exceptions;
using Case.Domain.Repositories;
using Case.Domain.ValueObjects;
using MediatR;

namespace Case.Application;

public static class ChangeReservationLicencePlate
{
    public sealed record Command(Guid ReservationId, string LicencePlate) : IRequest;
    
    internal class Handler : IRequestHandler<Command>
    {
        private readonly IWeeklyParkingSpotRepository _repository;

        public Handler(IWeeklyParkingSpotRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var reservationId = request.ReservationId;
            var licencePlate = request.LicencePlate;
            
            var weeklyParkingSpots = (await _repository.GetAllAsync());
            
            var weeklyParkingSpot = weeklyParkingSpots
                .FirstOrDefault(spot => spot.Reservations.Any(res => CompareIds(res.Id, reservationId)));
            
            if (weeklyParkingSpot == null)
            {
                throw new WeeklyParkingSpotNotFoundException(reservationId);
            }

            var reservation = weeklyParkingSpot.Reservations
                .FirstOrDefault(res => CompareIds(res.Id, reservationId));

            if (reservation == null)
            {
                throw new ReservationNotFoundException(reservationId);
            }
            
            var updatedReservation = reservation.WithNewLicencePlate(licencePlate);
            weeklyParkingSpot.UpdateReservation(updatedReservation);
            
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
        
        private static bool CompareIds(ReservationId id, Guid guid)
        {
            return id.Value == guid;
        }
    
    }

}