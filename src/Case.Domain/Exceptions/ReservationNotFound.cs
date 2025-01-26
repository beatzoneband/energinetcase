namespace Case.Domain.Exceptions;

public class ReservationNotFoundException : CustomException
{
    public Guid ReservationId { get; private set; }

    public ReservationNotFoundException(Guid reservationId) : base($"Reservation with ID: '{reservationId}' was not found.")
    {
        ReservationId = reservationId;
    }
}