﻿using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services;

public class ScheduleService : BaseEntityService<IAppUnitOfWork, IScheduleRepository, BLLAppDTO.ScheduleDto, DALAppDTO.ScheduleDto>, IScheduleService
{
    public ScheduleService(IAppUnitOfWork serviceUow, IScheduleRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ScheduleMapper(mapper))
    {
    }

}