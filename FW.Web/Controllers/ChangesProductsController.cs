using FW.BusinessLogic.Contracts;
using FW.Web.RpcClients.Interfaces;
using FW.Web.ViewModels.ChangesProducts;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodChangesProducts.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ChangesProductsController : ControllerBase
    {
        private readonly IChangesProductsRpcClient _rpcClient;

        public ChangesProductsController(IChangesProductsRpcClient rpcClient) => _rpcClient = rpcClient;
        Guid UserId => Guid.Parse(User.FindFirst(JwtClaimTypes.Subject).Value);

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var response = await _rpcClient.GetById(id, UserId);

            if (response.Id == Guid.Empty)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPage(UInt16 pageNumber, UInt16 pageSize)
        {
            if (pageNumber == 0 || pageSize == 0)
                return BadRequest();

            var response = await _rpcClient.GetPage(pageNumber, pageSize, UserId);

            if (response.Count == 0)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _rpcClient.GetAll(UserId);

            if (response.Count == 0)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Count()
        {
            var response = await _rpcClient.Count(UserId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}

