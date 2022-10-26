using Microsoft.AspNetCore.Mvc;
using FW.BusinessLogic.Contracts;
using FW.Web.RequestClients.Interfaces;
using FW.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityModel;

namespace FW.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRequestClient _requestClient;

        public CategoriesController(ICategoriesRequestClient requestClient) => _requestClient = requestClient;
        Guid UserId => Guid.Parse(User.FindFirst(JwtClaimTypes.Subject).Value);

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResponseVM>> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var response = await _requestClient.Get(id, UserId);

            if (response.Id == Guid.Empty)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryResponseVM>>> GetPage(UInt16 pageNumber, UInt16 pageSize)
        {
            if (pageNumber == 0 || pageSize == 0)
                return BadRequest();

            var response = await _requestClient.GetPage(pageNumber, pageSize, UserId);

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
            var response = await _requestClient.GetAll(UserId);

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
            var response = await _requestClient.Count(UserId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseStatusResult>> Add(CategoryVM category)
        {
            if (category == null || category.Name.Length == 0)
                return BadRequest();

            var response = await _requestClient.Create(category, UserId);

            if (response.Status == StatusResult.Error)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(response);
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseStatusResult>> Edit(Guid id, CategoryVM category)
        {
            if (id == Guid.Empty || category == null || category.Name.Length == 0)
                return BadRequest();

            var response = await _requestClient.Update(id, category, UserId);

            if (response.Status == StatusResult.NotFound)
                return NotFound();

            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseStatusResult>> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var response = await _requestClient.Delete(id, UserId);

            if (response.Status == StatusResult.NotFound)
                return NotFound();

            return Ok(response);
        }
    }
}