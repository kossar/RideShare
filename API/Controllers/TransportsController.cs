using API.DTO.v1.Mappers;
using AutoMapper;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for Transports - data, that connects transport offer and transport need.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransportsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private API.DTO.v1.Mappers.TransportMapper _transportMapper;

        /// <summary>
        /// Transports controller constructor. 
        /// </summary>
        /// <param name="transportMapper"></param>
        /// <param name="bll"></param>
        public TransportsController(IMapper mapper, IAppBLL bll)
        {
            _transportMapper = new TransportMapper(mapper);
            _bll = bll;
        }

        // GET: api/Transports
        /// <summary>
        /// Get all Transport that are connected to User
        /// </summary>
        /// <returns>List of PublicApi.DTO.v1.Transport</returns>
        [HttpGet("Latest")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<API.DTO.v1.Models.Ad.TransportAdListModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<API.DTO.v1.Models.Ad.TransportAdListModel>>> GetTransports([FromQuery] int count, CancellationToken cancellationToken)
        {
            Guid? userId = User.GetUserId() != null ? User.GetUserId()!.Value : null;
            return Ok((await _bll.Transports.GetLastOffersAndNeedsByCount(count, userId)).Select(t => _transportMapper.MapToBll(t)));
        }

    }


}
