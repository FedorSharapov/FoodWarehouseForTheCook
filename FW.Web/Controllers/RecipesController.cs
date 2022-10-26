using FW.BusinessLogic.Contracts;
using FW.Web.RpcClients.Interfaces;
using FW.Web.ViewModels.Recipes;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FW.Web.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesRpcClient _rpcClient;
        public RecipesController(IRecipesRpcClient rpcClient) => _rpcClient = rpcClient;

        Guid GetUserId => Guid.Parse(User.FindFirst(JwtClaimTypes.Subject).Value);

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var response = await _rpcClient.GetById(id, GetUserId);

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

            var response = await _rpcClient.GetPage(pageNumber, pageSize, GetUserId);

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
            var response = await _rpcClient.GetAll(GetUserId);

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
            var response = await _rpcClient.Count(GetUserId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(RecipeVM recipe)
        {
            if (recipe == null || recipe.DishesId == Guid.Empty || recipe.IngredientId == Guid.Empty)
                return BadRequest();

            var response = await _rpcClient.Create(recipe, GetUserId);

            if (response.Status == StatusResult.Error)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(response);
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(Guid id, RecipeVM recipe)
        {
            if (id == Guid.Empty || recipe == null || recipe.DishesId == Guid.Empty || recipe.IngredientId == Guid.Empty)
                return BadRequest();

            var response = await _rpcClient.Update(id, recipe, GetUserId);

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

            var response = await _rpcClient.Delete(id, GetUserId);

            if (response.Status == StatusResult.NotFound)
                return NotFound();

            return Ok(response);
        }
    }
}
