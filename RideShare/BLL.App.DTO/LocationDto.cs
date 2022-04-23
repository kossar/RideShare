using BLL.App.DTO.Common;

namespace BLL.App.DTO
{
    public class LocationDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Country { get; set; } = default!;
        public string? Province { get; set; }
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? Description { get; set; }
        public virtual string FullAddress => $"{Address}, {City}, {Province ?? Country}{(Province != null ? ", " + Country : string.Empty)}";

    }
}