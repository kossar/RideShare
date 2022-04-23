using API.DTO.v1.Models;
using API.DTO.v1.Mappers;
using API.DTO.v1.Models;
using API.DTO.v1.Models.TransportOffer;
using AutoMapper;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

     /// <summary>
     /// TransportOffers controller
     /// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TransportOffersController : ControllerBase
{
    private readonly IAppBLL _bll;
    private API.DTO.v1.Mappers.TransportOfferMapper _transportOfferMapper;

    /// <summary>
    /// TransportOffers controller constructor
    /// </summary>
    /// <param name="bll">IAppBLL</param>
    /// <param name="mapper">Mapper to map TransportOffer entity between BLL and PublicApi layers</param>
    public TransportOffersController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _transportOfferMapper = new TransportOfferMapper(mapper);
    }

    // GET: api/TransportOffers
    /// <summary>
    /// Get all TransportOffers
    /// </summary>
    ///// <returns>List of API.DTO.v1.Models.TransportOffer.TransportOfferDto</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<API.DTO.v1.Models.TransportOffer.TransportOfferModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<API.DTO.v1.Models.TransportOffer.TransportOfferModel>>> GetTransportOffers()
    {
        var res = (await _bll.TransportOffers.GetAllAsync())
            .Select(t => _transportOfferMapper.Map(t));
        var listItems = res.Select(x => _transportOfferMapper.Map(x));
        return Ok(listItems);

    }

    //// GET: api/TransportOffers
    ///// <summary>
    ///// Get all User TransportOffers
    ///// </summary>
    ///// <returns>List of API.DTO.v1.Models.TransportOffer.TransportOfferDto</returns>
    //[HttpGet("user")]
    //[AllowAnonymous]
    //[Produces("application/json")]
    //[ProducesResponseType(typeof(List<TransportOfferDto>), StatusCodes.Status200OK)]
    //public async Task<ActionResult<IEnumerable<TransportOfferDto>>> GetUserTransportOffers()
    //{
    //    var res = (await _bll.TransportOffers.GetAllAsync(User.GetUserId()!.Value))
    //        .Select(t => _transportOfferMapper.Map(t));
    //    var listItems = res.Select(TransportOfferMapper.MapToListItem);
    //    // return Ok((await _bll.TransportOffers.GetAllAsync(User.GetUserId()!.Value))
    //    //     .Select(t => _transportOfferMapper.Map(t)));
    //    return Ok(listItems);
    //}

    // GET: api/TransportOffers/5
    /// <summary>
    /// Get all TransportOffers
    /// </summary>
    /// <param name="id">GUID</param>
    /// <returns>List of TransportOffers</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<TransportOfferModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<TransportOfferModel>> GetTransportOffer(Guid id)
    {

        var transportOffer = await _bll.TransportOffers.FirstOrDefaultAsync(id);

        if (transportOffer == null)
        {
            return NotFound(new Message("Cannot find this id: " + id));
        }

        return _transportOfferMapper.Map(transportOffer)!;
    }

    // PUT: api/TransportOffers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update TransportOffer
    /// </summary>
    /// <param name="id">GUID</param>
    /// <param name="transportOffer">API.DTO.v1.Models.TransportOffer.TransportOfferDto</param>
    /// <returns>NoContent</returns>
    //[HttpPut("{id}")]
    //[Consumes("application/json")]
    //[Produces("application/json")]
    //[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    //[ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    //public async Task<IActionResult> PutTransportOffer(Guid id, TransportOfferDto transportOffer)
    //{
    //    if (id != transportOffer.Id)
    //    {
    //        return BadRequest(new Message("Cannot update. Id: " + id + " not found."));
    //    }

    //    var bllTransportOffer = _transportOfferMapper.Map(transportOffer);
    //    if (bllTransportOffer!.Trailer != null)
    //    {
    //        bllTransportOffer.Trailer.AppUserId = User.GetUserId()!.Value;
    //    }

    //    bllTransportOffer.Vehicle!.AppUserId = User.GetUserId()!.Value;
    //    _bll.TransportOffers.Update(bllTransportOffer!);
    //    await _bll.SaveChangesAsync();

    //    return NoContent();
    //}

    // POST: api/TransportOffers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Save new TransportOffer
    /// </summary>
    /// <param name="transportOffer">API.DTO.v1.Models.TransportOffer.TransportOfferDtoAdd</param>
    /// <returns>API.DTO.v1.Models.TransportOffer.TransportOfferDto</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TransportOfferModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TransportOfferModel>> PostTransportOffer(API.DTO.v1.Models.TransportOffer.TransportOfferAddModel transportOffer)
    {
        var bllTransportOffer = _transportOfferMapper.MapToBll(transportOffer);
        bllTransportOffer.UserId = User.GetUserId()!.Value;
        var addedTransportOffer = _bll.TransportOffers.Add(bllTransportOffer);
        await _bll.SaveChangesAsync();

        var returnTransportOffer = _transportOfferMapper.Map(addedTransportOffer);
        return CreatedAtAction("GetTransportOffer", new { id = returnTransportOffer!.Id }, returnTransportOffer);
    }

    // DELETE: api/TransportOffers/5
    /// <summary>
    /// Delete TransportOffer
    /// </summary>
    /// <param name="id">GUID</param>
    /// <returns>NoContent</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteTransportOffer(Guid id)
    {
        var transportOffer = await _bll.TransportOffers.FirstOrDefaultAsync(id);
        if (transportOffer == null)
        {
            return NotFound(new Message("Cant delete. Cannot find id: " + id));
        }

        _bll.TransportOffers.Remove(transportOffer, User.GetUserId()!.Value);
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> TransportOfferExists(Guid id)
    {
        return await _bll.TransportOffers.ExistsAsync(id);
    }
}
