﻿using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services;

public class VehicleService : BaseEntityService<IAppUnitOfWork, IVehicleRepository, BLLAppDTO.VehicleDto, DALAppDTO.VehicleDto>, IVehicleService
{
    public VehicleService(IAppUnitOfWork serviceUow, IVehicleRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new VehicleMapper(mapper))
    {
    }

}