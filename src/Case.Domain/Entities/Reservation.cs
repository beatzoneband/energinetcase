using Case.Domain.ValueObjects;

namespace Case.Domain.Entities;

public sealed record Reservation(
    ReservationId Id,
    UserId UserId,
    EmployeeName EmployeeName,
    LicensePlate LicensePlate,
    Date Date)
{

    public static Reservation Create(ReservationId reservationId, UserId userId, EmployeeName name, 
        LicensePlate licensePlate, Date date ) 
        => new(reservationId, userId, name, licensePlate, date);

    public Reservation WithNewLicensePlate(string newLicensePlate)
    {
        return this with { LicensePlate = new LicensePlate(newLicensePlate) };
    }
}