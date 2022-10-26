using AutoMapper;
using FW.BusinessLogic.Contracts.Dishes;
using FW.EventBus.Interfaces;
using System.Text.Json;
using FW.Web.RpcClients.Interfaces;
using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Dishes;
using FW.RabbitMQOptions;

namespace FW.Web.RpcClients
{
    public class DishesRpcClient : RpcClientBase, IDishesRpcClient
    {
        private readonly IMapper _mapper;
        private readonly string _exchangeName;
        private readonly QueueNamesDishes _queueNames;

        public DishesRpcClient(IMapper mapper, IConnectionRabbitMQ connection, IConfiguration configuration) :
            base(connection, configuration)
        {
            _mapper = mapper;

            var exchangeNames = configuration.GetSection(RabbitMqExchangeNamesOptions.KeyValue).Get<RabbitMqExchangeNamesOptions>();
            _exchangeName = exchangeNames.Dishes;

            var queueNames = configuration.GetSection(RabbitMqQueueNamesOptions.KeyValue).Get<RabbitMqQueueNamesOptions>();
            _queueNames = queueNames.Dishes;

            Parallel.ForEach(_queueNames.AllNames, qName =>
                ConfigureRpcClient(_exchangeName, qName));
        }

        public async Task<DishResponseVM> GetById(Guid id, Guid userId)
        {
            var queryDto = new DishGetByIdDto { Id = id, UserId = userId};
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Get, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<DishResponseDto>(responseJsonDto);

            var dishResponseVM = _mapper.Map<DishResponseVM>(responseDto);
            return dishResponseVM;
        }

        public async Task<List<DishResponseVM>> GetPage(ushort pageNumber, ushort pageSize, Guid userId)
        {
            var queryDto = new DishesGetPageDto { PageNumber = pageNumber, PageSize = pageSize, UserId = userId};
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.GetPage, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<DishesResponseDto>(responseJsonDto);

            var dishes = _mapper.Map<List<DishResponseVM>>(responseDto?.Dishes);
            return dishes;
        }

        public async Task<List<DishResponseVM>> GetAll(Guid userId)
        {
            var queryDto = new DishesGetAllDto { UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.GetAll, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<DishesResponseDto>(responseJsonDto);

            var dishes = _mapper.Map<List<DishResponseVM>>(responseDto?.Dishes);
            return dishes;
        }

        public async Task<int> Count(Guid userId)
        {
            var queryDto = new DishesGetCountDto{UserId = userId};
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Count, queryJsonDto);

            return JsonSerializer.Deserialize<int>(responseJsonDto);
        }

        public async Task<ResponseStatusResult> Create(DishVM dish, Guid userId)
        {
            var queryDto = _mapper.Map<DishCreateDto>(dish);
            queryDto.UserId = userId;

            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Create, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResult>(responseJsonDto);

            return responseDto;
        }

        public async Task<ResponseStatusResult> Update(Guid id, DishVM dish, Guid userId)
        {
            var queryDto = _mapper.Map<DishUpdateDto>(dish);
            queryDto.Id = id;
            queryDto.UserId = userId;

            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Update, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResult>(responseJsonDto);

            return responseDto;
        }

        public async Task<ResponseStatusResult> Delete(Guid id, Guid userId)
        {
            var queryDto = new DishDeleteDto{Id = id, UserId = userId};

            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Delete, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResult>(responseJsonDto);

            return responseDto;
        }

        public async Task<ResponseStatusResult> Cook(Guid id, Guid userId, int numPortions)
        {
            var queryDto = new DishCookDto { Id = id, UserId = userId , NumPortions = numPortions };

            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Cook, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResult>(responseJsonDto);

            return responseDto;
        }
    }
}
