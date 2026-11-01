namespace ASPNET_Practice.Models
{
    public record Car(
        int id,
        string Brand,
        string Model,
        DateOnly ProductionDate,
        int? HorsePower,
        CarBodyType? BodyType,
        string VIN
        );
    
}
