using API.DTO.v1.Models.Vehicle;
using AutoMapper;

namespace API.DTO.v1.Mappers;

public class VehicleMapper : BaseMapper<VehicleModel, BLL.App.DTO.VehicleDto>
{
    private readonly IMapper _mapper;
    public VehicleMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.VehicleDto MapToBll(VehicleModel vehicleAdd)
    {
        var bllVehicle = _mapper.Map<BLL.App.DTO.VehicleDto>(vehicleAdd);
        return bllVehicle;
    }

    public BLL.App.DTO.VehicleDto MapToBll(CreateUpdateVehicleModel createUpdateVehicle)
    {
        var bllVehicle = _mapper.Map<BLL.App.DTO.VehicleDto>(createUpdateVehicle);
        return bllVehicle;
    }
}