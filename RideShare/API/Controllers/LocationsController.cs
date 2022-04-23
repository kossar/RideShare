using API.DTO.v1.Mappers;
using API.DTO.v1.Models;
using API.DTO.v1.Models.Location;
using AutoMapper;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers;

/// <summary>
/// Locations controller. API endpoints for API.DTO.v1.Locations Create, Read, Update and Delete. 
/// </summary>
[ApiVersion("1.0")]
[Route("API/v{version:ApiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LocationsController : ControllerBase
{
    private readonly IAppBLL _bll;

    private API.DTO.v1.Mappers.LocationMapper _locationMapper;

    /// <summary>
    /// Locations controller constructor.
    /// </summary>
    /// <param name="bll">IAppBLL</param>
    /// <param name="mapper">Mapper to map between BLL and API Location</param>
    public LocationsController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _locationMapper = new LocationMapper(mapper);
    }

    // GET: API/Locations
    /// <summary>
    /// Gets all locations.
    /// </summary>
    /// <returns>List of API.DTO.v1.Locations</returns>
    [AllowAnonymous]
    [HttpGet]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<LocationAddModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<LocationAddModel>>> GetLocations()
    {
        return Ok((await _bll.Locations.GetAllAsync())
            .Select(l => _locationMapper.Map(l)));
    }

    // GET: API/Locations/5
    /// <summary>
    /// Get one location.
    /// </summary>
    /// <param name="id">GUID</param>
    /// <returns>API.DTO.v1.Location</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(LocationAddModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LocationAddModel>> GetLocation(Guid id)
    {
        var location = await _bll.Locations.FirstOrDefaultAsync(id);

        if (location == null)
        {
            return NotFound(new Message("Cannot find this id: " + id));
        }

        return _locationMapper.Map(location)!;
    }

    // PUT: API/Locations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update location.
    /// </summary>
    /// <param name="id">GUID</param>
    /// <param name="location">API.DTO.v1.Location</param>
    /// <returns>NoContent</returns>
    [HttpPut("{id}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutLocation(Guid id, LocationAddModel location)
    {
        if (id != location.Id)
        {
            return BadRequest(new Message("Cannot update. Id: " + id + " not found."));
        }

        var bllLocation = _locationMapper.Map(location)!;

        _bll.Locations.Update(bllLocation);


        await _bll.SaveChangesAsync();


        return NoContent();
    }

    // POST: API/Locations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Saves new location.
    /// </summary>
    /// <param name="location">API.DTO.v1.LocationAdd </param>
    /// <returns>API.DTO.v1.Location</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(LocationAddModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LocationAddModel>> PostLocation(LocationAddModel location)
    {
        var bllLocation = _locationMapper.MapToBll(location);
        bllLocation.UserId = User.GetUserId()!.Value;
        var addedLocation = _bll.Locations.Add(bllLocation);
        await _bll.SaveChangesAsync();
        var returnLocation = _locationMapper.Map(addedLocation);

        return CreatedAtAction("GetLocation", new { id = returnLocation!.Id }, returnLocation);
    }

    // DELETE: API/Locations/5
    /// <summary>
    /// Deletes location.
    /// </summary>
    /// <param name="id">GUID</param>
    /// <returns>NoContent</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLocation(Guid id)
    {
        var location = await _bll.Locations.FirstOrDefaultAsync(id);
        if (location == null)
        {
            return NotFound(new Message("Cant delete. Cannot find id: " + id));
        }

        _bll.Locations.Remove(location);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}