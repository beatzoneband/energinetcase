using Case.Domain.ValueObjects;

namespace Case.Domain.Entities;

public sealed record Reservation(
    ReservationId Id,
    UserId UserId,
    EmployeeName EmployeeName,
    LicencePlate LicencePlate,
    Date Date)
{

    public static Reservation Create(ReservationId reservationId, UserId userId, EmployeeName name, 
        LicencePlate licencePlate, Date date ) 
        => new(reservationId, userId, name, licencePlate, date);

    public Reservation WithNewLicencePlate(string newLicencePlate)
    {
        return this with { LicencePlate = new LicencePlate(newLicencePlate) };
    }
}