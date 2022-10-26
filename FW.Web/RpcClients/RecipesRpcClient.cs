using AutoMapper;
using FW.BusinessLogic.Contracts.Recipes;
using FW.EventBus.Interfaces;
using System.Text.Json;
using FW.Web.RpcClients.Interfaces;
using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Recipes;
using FW.RabbitMQOptions;

namespace FW.Web.RpcClients
{
    public class RecipesRpcClient : RpcClientBase, IRecipesRpcClient
    {
        private readonly IMapper _mapper;
        private readonly string _exchangeName;
        private readonly QueueNames _queueNames;

        public RecipesRpcClient(IMapper mapper, IConnectionRabbitMQ connection, IConfiguration configuration) :
            base(connection, configuration)
        {
            _mapper = mapper;

            var exchangeNames = configuration.GetSection(RabbitMqExchangeNamesOptions.KeyValue).Get<RabbitMqExchangeNamesOptions>();
            _exchangeName = exchangeNames.Recipes;

            var queueNames = configuration.GetSection(RabbitMqQueueNamesOptions.KeyValue).Get<RabbitMqQueueNamesOptions>();
            _queueNames = queueNames.Recipes;

            Parallel.ForEach(_queueNames.AllNames, qName =>
                ConfigureRpcClient(_exchangeName, qName));
        }

        public async Task<RecipeResponseVM> GetById(Guid id, Guid userId)
        {
            var queryDto = new RecipeGetByIdDto { Id = id, UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Get, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<RecipeResponseDto>(responseJsonDto);

            var recipeResponseVM = _mapper.Map<RecipeResponseVM>(responseDto);
            return recipeResponseVM;
        }

        public async Task<List<RecipeResponseVM>> GetPage(ushort pageNumber, ushort pageSize, Guid userId)
        {
            var queryDto = new RecipesGetPageDto { PageNumber = pageNumber, PageSize = pageSize, UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.GetPage, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<RecipesResponseDto>(responseJsonDto);

            var recipes = _mapper.Map<List<RecipeResponseVM>>(responseDto?.Recipes);
            return recipes;
        }

        public async Task<List<RecipeResponseVM>> GetAll(Guid userId)
        {
            var queryDto = new RecipesGetAllDto { UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.GetAll, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<RecipesResponseDto>(responseJsonDto);

            var recipes = _mapper.Map<List<RecipeResponseVM>>(responseDto?.Recipes);
            return recipes;
        }

        public async Task<int> Count(Guid userId)
        {
            var queryDto = new RecipesGetCountDto { UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Count, queryJsonDto);

            return JsonSerializer.Deserialize<int>(responseJsonDto);
        }

        public async Task<ResponseStatusResult> Create(RecipeVM recipe, Guid userId)
        {
            var queryDto = _mapper.Map<RecipeCreateDto>(recipe);
            queryDto.UserId = userId;

            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Create, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResult>(responseJsonDto);

            return responseDto;
        }

        public async Task<ResponseStatusResult> Update(Guid id, RecipeVM recipe, Guid userId)
        {
            var queryDto = _mapper.Map<RecipeUpdateDto>(recipe);
            queryDto.Id = id;
            queryDto.UserId = userId;

            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Update, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResult>(responseJsonDto);

            return responseDto;
        }

        public async Task<ResponseStatusResult> Delete(Guid id, Guid userId)
        {
            var queryDto = new RecipeDeleteDto { Id = id, UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Delete, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResult>(responseJsonDto);

            return responseDto;
        }
    }
}
