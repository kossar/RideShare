using API.DTO.v1.Mappers;
using API.DTO.v1.Models;
using API.DTO.v1.Models.Vehicle;
using AutoMapper;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Vehicle API controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class VehiclesController : ControllerBase
{
    private readonly IAppBLL _bll;
    private API.DTO.v1.Mappers.VehicleMapper _vehicleMapper;

    /// <summary>
    /// Vehicle controller constructor
    /// </summary>
    /// <param name="bll">IAppBLL</param>
    /// <param name="mapper">Mapper to map between BLL and API Vehicle</param>
    public VehiclesController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _vehicleMapper = new VehicleMapper(mapper);
    }

    // GET: api/Vehicles
    /// <summary>
    /// Get Vehicles
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<VehicleModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VehicleModel>>> GetVehicles()
    {
        Console.WriteLine("APi call");
        return Ok((await _bll.Vehicles.GetAllAsync(User.GetUserId()!.Value))
            .Select(v => _vehicleMapper.Map(v)));
    }

    // GET: api/Vehicles/5
    /// <summary>
    /// Get Vehicle by Id
    /// </summary>
    /// <param name="id">GUID</param>
    /// <returns>API.DTO.v1.Models.Vehicle.VehicleDto</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(VehicleModel), StatusCodes.Status201Created)]
    public async Task<ActionResult<VehicleModel>> GetVehicle(Guid id)
    {
        var vehicle = await _bll.Vehicles.FirstOrDefaultAsync(id);

        if (vehicle == null)
        {
            return NotFound(new Message("Cannot find this id: " + id));
        }

        var returnVehicle = _vehicleMapper.Map(vehicle)!;
        return returnVehicle;
    }

    // PUT: api/Vehicles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update Vehicle
    /// </summary>
    /// <param name="id">GUID</param>
    /// <param name="vehicle">API.DTO.v1.Models.Vehicle.VehicleDto</param>
    /// <returns>NoContent</returns>
    [HttpPut("{id}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PutVehicle(Guid id, VehicleModel vehicle)
    {
        if (id != vehicle.Id)
        {
            return BadRequest(new Message("Cannot update. Id: " + id + " not found."));
        }

        var bllVehicle = _vehicleMapper.Map(vehicle)!;
        _bll.Vehicles.Update(bllVehicle);

        await _bll.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Vehicles
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Save new Vehicle
    /// </summary>
    /// <param name="createUpdateVehicle">API.DTO.v1.Models.Vehicle.VehicleDtoAdd</param>
    /// <returns>API.DTO.v1.Models.Vehicle.VehicleDto</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(VehicleModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<VehicleModel>> PostVehicle([FromBody] API.DTO.v1.Models.Vehicle.CreateUpdateVehicleModel createUpdateVehicle)
    {
        var bllVehicle = _vehicleMapper.MapToBll(createUpdateVehicle);
        bllVehicle.UserId = User.GetUserId()!.Value;

        var addedVehicle = _bll.Vehicles.Add(bllVehicle);
        await _bll.SaveChangesAsync();


        var returnVehicle = _vehicleMapper.Map(addedVehicle);

        return CreatedAtAction("GetVehicle", new { id = returnVehicle!.Id }, returnVehicle);
    }

    // DELETE: api/Vehicles/5
    /// <summary>
    /// Delete Vehicle
    /// </summary>
    /// <param name="id">GUID</param>
    /// <returns>NoContent</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehicleModel))]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<VehicleModel>> DeleteVehicle(Guid id)
    {
        var vehicle = await _bll.Vehicles.FirstOrDefaultAsync(id);
        if (vehicle == null)
        {
            return NotFound(new Message("Cant delete. Cannot find id: " + id));
        }

        var deletedVehicle = await _bll.Vehicles.RemoveAsync(id, User.GetUserId()!.Value);
        await _bll.SaveChangesAsync();

        var returnVehicle = _vehicleMapper.Map(deletedVehicle);

        return Ok(returnVehicle);
    }
}