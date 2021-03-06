namespace API.DTO.v1.Models.Vehicle;

public class CreateUpdateVehicleModel
{
    public Guid? Id { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string Number { get; set; } = default!;
}