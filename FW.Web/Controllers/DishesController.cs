using FW.BusinessLogic.Contracts;
using FW.Web.RpcClients.Interfaces;
using FW.Web.ViewModels.Dishes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IdentityModel;

namespace FW.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IDishesRpcClient _rpcClient;

        public DishesController(IDishesRpcClient rpcClient) => _rpcClient = rpcClient;

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

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(DishVM dish)
        {
            if (dish == null || dish.Name.Length == 0)
                return BadRequest();

            var response = await _rpcClient.Create(dish, UserId);

            if (response.Status == StatusResult.Error)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(response);
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(Guid id, DishVM dish)
        {
            if (id == Guid.Empty || dish == null || dish.Name.Length == 0)
                return BadRequest();

            var response = await _rpcClient.Update(id, dish, UserId);

            if (response.Status == StatusResult.NotFound)
                return NotFound();

            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var response = await _rpcClient.Delete(id, UserId);

            if (response.Status == StatusResult.NotFound)
                return NotFound();

            return Ok(response);
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cook(Guid id, int numPortions)
        {
            if (id == Guid.Empty || numPortions == 0)
                return BadRequest();

            var response = await _rpcClient.Cook(id, UserId, numPortions);

            if (response.Status == StatusResult.NotFound)
                return NotFound(response.Title);

            return Ok(response);
        }
    }
}
