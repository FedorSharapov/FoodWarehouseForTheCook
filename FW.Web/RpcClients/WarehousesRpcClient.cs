using AutoMapper;
using FW.BusinessLogic.Contracts.Warehouses;
using FW.EventBus.Interfaces;
using System.Text.Json;
using FW.Web.RpcClients.Interfaces;
using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Warehouses;
using FW.RabbitMQOptions;

namespace FW.Web.RpcClients
{
    public class WarehousesRpcClient : RpcClientBase, IWarehousesRpcClient
    {
        private readonly IMapper _mapper;
        private readonly string _exchangeName;
        private readonly QueueNamesWithGetByUserId _queueNames;

        public WarehousesRpcClient(IMapper mapper, IConnectionRabbitMQ connection, IConfiguration configuration) :
            base(connection, configuration)
        {
            _mapper = mapper;

            var exchangeNames = configuration.GetSection(RabbitMqExchangeNamesOptions.KeyValue).Get<RabbitMqExchangeNamesOptions>();
            _exchangeName = exchangeNames.Warehouses;

            var queueNames = configuration.GetSection(RabbitMqQueueNamesOptions.KeyValue).Get<RabbitMqQueueNamesOptions>();
            _queueNames = queueNames.Warehouses;

            Parallel.ForEach(_queueNames.AllNames, qName =>
            {
                if(qName !="") ConfigureRpcClient(_exchangeName, qName);
            });
        }

        public async Task<WarehouseResponseVM> GetByUserId(Guid userId)
        {
            var queryDto = new WarehouseGetByUserIdDto {UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.GetByUserId, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<WarehouseResponseDto>(responseJsonDto);

            var warehouseResponseVM = _mapper.Map<WarehouseResponseVM>(responseDto);
            return warehouseResponseVM;
        }

        public async Task<ResponseStatusResultWithoutId> Create(WarehouseVM warehouse, Guid userId)
        {
            var queryDto = _mapper.Map<WarehouseCreateDto>(warehouse);
            queryDto.UserId = userId;

            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Create, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResultWithoutId>(responseJsonDto);
            return responseDto;
        }

        public async Task<ResponseStatusResultWithoutId> Update(WarehouseVM warehouse, Guid userId)
        {
            var queryDto = _mapper.Map<WarehouseUpdateDto>(warehouse);
            queryDto.UserId = userId;

            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Update, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ResponseStatusResultWithoutId>(responseJsonDto);
            return responseDto;
        }
    }
}
