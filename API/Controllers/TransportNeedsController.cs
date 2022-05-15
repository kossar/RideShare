using API.DTO.v1.Models;
using API.DTO.v1.Mappers;
using AutoMapper;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


/// <summary>
/// TransportNeeds controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TransportNeedsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly API.DTO.v1.Mappers.TransportNeedMapper _transportNeedMapper;

    /// <summary>
    /// TransportNeeds controller constructor.
    /// </summary>
    /// <param name="bll">IAppBLL</param>
    /// <param name="mapper">Mapper to map between BLL and PublicApi TransportNeed</param>
    public TransportNeedsController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _transportNeedMapper = new TransportNeedMapper(mapper);
    }

    // GET: api/TransportNeeds
    /// <summary>
    /// Gets all TransportNeeds that contains User id.
    /// </summary>
    /// <returns>List of API.DTO.v1.Models.TransportNeed.TransportNeedDto</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<API.DTO.v1.Models.Ad.TransportAdListModel>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<API.DTO.v1.Models.Ad.TransportAdListModel>>> GetTransportNeeds()
    {
        Guid userId = User.GetUserId() != null ? User.GetUserId()!.Value : default;
        return Ok((await _bll.TransportNeeds.GetAllWithIncludingsAsync(userId))
            .Select(n => _transportNeedMapper.MapTransportNeedToAdListItem(n, userId)));
    }

    // GET: api/TransportNeeds
    /// <summary>
    /// Gets all TransportNeeds that contains User id.
    /// </summary>
    /// <returns>List of API.DTO.v1.Models.TransportNeed.TransportNeedDto</returns>
    //[HttpGet("user")]
    //[Produces("application/json")]
    //[ProducesResponseType(typeof(IEnumerable<API.DTO.v1.Models.Ad.TransportAdListModel>), StatusCodes.Status200OK)]
    //public async Task<ActionResult<IEnumerable<API.DTO.v1.Models.Ad.TransportAdListModel>>> GetUserTransportNeeds()
    //{
    //    return Ok((await _bll.TransportNeeds.GetAllWithIncludingsAsync(User.GetUserId()!.Value))
    //        .Select(n => _transportNeedMapper.Map(n)));
    //}

    // GET: api/TransportNeeds/5
    /// <summary>
    /// Gets TransportNeed by ID
    /// </summary>
    /// <param name="id">GUID</param>
    /// <returns>API.DTO.v1.Models.TransportNeed.TransportNeedDto</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(API.DTO.v1.Models.TransportNeed.TransportNeedModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(API.DTO.v1.Models.Message), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<ActionResult<API.DTO.v1.Models.TransportNeed.TransportNeedModel>> GetTransportNeed(Guid id)
    {
        var bllTransportNeed = await _bll.TransportNeeds.FirstOrDefaultAsync(id, User.GetUserId()!.Value); // TODO: check

        if (bllTransportNeed == null)
        {
            return NotFound(new Message("Cannot find this id: " + id));
        }

        var returnTransportNeed = _transportNeedMapper.Map(bllTransportNeed)!;
        return returnTransportNeed;
    }

    // PUT: api/TransportNeeds/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update TransportNeed.
    /// </summary>
    /// <param name="id">GUID</param>
    /// <param name="transportNeed">API.DTO.v1.Models.TransportNeed.TransportNeedDto</param>
    /// <returns>NoContent</returns>
    [HttpPut("{id}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(API.DTO.v1.Models.Message), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PutTransportNeed(Guid id, API.DTO.v1.Models.TransportNeed.TransportNeedModel transportNeed)
    {
        if (id != transportNeed.Id)
        {
            return BadRequest(new Message("Cannot update. Id: " + id + " not found."));
        }

        var bllTransportNeed = _transportNeedMapper.Map(transportNeed)!;
        _bll.TransportNeeds.Update(bllTransportNeed);
        await _bll.SaveChangesAsync();


        return NoContent();
    }

    // POST: api/TransportNeeds
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Saves new TransportNeed.
    /// </summary>
    /// <param name="transportNeed"></param>
    /// <returns>API.DTO.v1.Models.TransportNeed.TransportNeedDto</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(API.DTO.v1.Models.TransportNeed.TransportNeedModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(API.DTO.v1.Models.Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<API.DTO.v1.Models.TransportNeed.TransportNeedModel>> PostTransportNeed(API.DTO.v1.Models.TransportNeed.CreateUpdateTransportNeedModel transportNeed)
    {
        //TODO: servicesess
        //if (transportNeed.StartLocation == null|| transportNeed.DestinationLocation == null)
        //{
        //    return BadRequest(new Message("Asukohad peavad olema täidetud"));
        //}

        var bllTransportNeed = _transportNeedMapper.MapToBll(transportNeed, User.GetUserId()!.Value);
        var addedTransportNeed = _bll.TransportNeeds.Add(bllTransportNeed);
        await _bll.SaveChangesAsync();

        var returnTransportNeed = _transportNeedMapper.Map(addedTransportNeed);

        return CreatedAtAction("GetTransportNeed", new { id = returnTransportNeed!.Id }, returnTransportNeed);
    }

    // DELETE: api/TransportNeeds/5
    /// <summary>
    /// Deletes TransportNeed
    /// </summary>
    /// <param name="id">GUID</param>
    /// <returns>NoContent</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(API.DTO.v1.Models.Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteTransportNeed(Guid id)
    {
        var transportNeed = await _bll.TransportNeeds.FirstOrDefaultAsync(id);
        if (transportNeed == null)
        {
            return NotFound(new Message("Cant delete. Cannot find id: " + id));
        }

        _bll.TransportNeeds.Remove(transportNeed, User.GetUserId()!.Value);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}