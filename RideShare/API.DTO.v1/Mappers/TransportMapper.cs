using API.DTO.v1.Models.Transport;
using AutoMapper;

namespace API.DTO.v1.Mappers;

public class TransportMapper : BaseMapper<TransportModel, BLL.App.DTO.TransportDto>
{
    private readonly IMapper _mapper;
    public TransportMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.TransportDto MapToBll(TransportModel TransportAdd)
    {
        var bllTransport = _mapper.Map<BLL.App.DTO.TransportDto>(TransportAdd);
        return bllTransport;
    }
}