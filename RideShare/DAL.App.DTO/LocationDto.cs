using DAL.App.DTO.Common;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class LocationDto : BaseDto
    {
        public Guid? UserId { get; set; }
        //public virtual UserDto? User { get; set; }
        public string Country { get; set; } = default!;
        public string? Province { get; set; }
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<TransportDto>? TransportStartLocations { get; set; }
        public ICollection<TransportDto>? TransportDestinationLocations { get; set; }
        public ICollection<TransportOfferDto>? TransportOfferStartLocations { get; set; }
        public ICollection<TransportOfferDto>? TransportOfferDestinationLocations { get; set; }
        public ICollection<TransportNeedDto>? TransportNeedStartLocations { get; set; }
        public ICollection<TransportNeedDto>? TransportNeedDestinationLocations { get; set; }
    }
}