using FW.BusinessLogic.Contracts;
using FW.Web.RpcClients.Interfaces;
using FW.Web.ViewModels.Warehouses;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FW.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehousesRpcClient _rpcClient;
        public WarehousesController(IWarehousesRpcClient rpcClient) => _rpcClient = rpcClient;

        Guid UserId => Guid.Parse(User.FindFirst(JwtClaimTypes.Subject).Value);

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var response = await _rpcClient.GetByUserId(UserId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(WarehouseVM warehouse)
        {
            if (warehouse == null || warehouse.Name.Length == 0)
                return BadRequest();
            
            var response = await _rpcClient.Create(warehouse, UserId);

            if (response.Status == StatusResult.Error)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(response);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(WarehouseVM warehouse)
        {
            if (warehouse == null || warehouse.Name.Length == 0)
                return BadRequest();

            var response = await _rpcClient.Update(warehouse, UserId);

            if (response.Status == StatusResult.NotFound)
                return NotFound();

            return Ok(response);
        }
    }
}
