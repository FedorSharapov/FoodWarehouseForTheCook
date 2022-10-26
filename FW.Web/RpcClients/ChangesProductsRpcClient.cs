using AutoMapper;
using FW.BusinessLogic.Contracts.ChangesProducts;
using FW.EventBus.Interfaces;
using System.Text.Json;
using FW.Web.RpcClients.Interfaces;
using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.ChangesProducts;
using FW.RabbitMQOptions;
using System.Threading;

namespace FW.Web.RpcClients
{
    public class ChangesProductsRpcClient : RpcClientBase, IChangesProductsRpcClient
    {
        private readonly IMapper _mapper;
        private readonly string _exchangeName;
        private readonly QueueNames _queueNames;

        public ChangesProductsRpcClient(IMapper mapper, IConnectionRabbitMQ connection, IConfiguration configuration) :
            base(connection, configuration)
        {
            _mapper = mapper;

            var exchangeNames = configuration.GetSection(RabbitMqExchangeNamesOptions.KeyValue).Get<RabbitMqExchangeNamesOptions>();
            _exchangeName = exchangeNames.ChangesProducts;

            var queueNames = configuration.GetSection(RabbitMqQueueNamesOptions.KeyValue).Get<RabbitMqQueueNamesOptions>();
            _queueNames = queueNames.ChangesProducts;

            Parallel.ForEach(_queueNames.AllNames, qName =>
                ConfigureRpcClient(_exchangeName, qName));
        }

        public async Task<ChangesProductResponseVM> GetById(Guid id, Guid userId)
        {
            var queryDto = new ChangesProductGetByIdDto { Id = id, UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Get, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ChangesProductResponseDto>(responseJsonDto);

            var changesProductResponseVM = _mapper.Map<ChangesProductResponseVM>(responseDto);
            return changesProductResponseVM;
        }

        public async Task<List<ChangesProductResponseVM>> GetPage(ushort pageNumber, ushort pageSize, Guid userId)
        {
            var queryDto = new ChangesProductsGetPageDto { PageNumber = pageNumber, PageSize = pageSize, UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);

            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.GetPage, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ChangesProductsResponseDto>(responseJsonDto);

            var changesProducts = _mapper.Map<List<ChangesProductResponseVM>>(responseDto?.ChangesProducts);
            return changesProducts;
        }

        public async Task<List<ChangesProductResponseVM>> GetAll(Guid userId)
        {
            var queryDto = new ChangesProductsGetAllDto { UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);
            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.GetAll, queryJsonDto);
            var responseDto = JsonSerializer.Deserialize<ChangesProductsResponseDto>(responseJsonDto);

            var changesProducts = _mapper.Map<List<ChangesProductResponseVM>>(responseDto?.ChangesProducts);
            return changesProducts;
        }

        public async Task<int> Count(Guid userId)
        {
            var queryDto = new ChangesProductsGetCountDto { UserId = userId };
            var queryJsonDto = JsonSerializer.Serialize(queryDto);
            var responseJsonDto = await CallAsync(_exchangeName, _queueNames.Count, queryJsonDto);
            return JsonSerializer.Deserialize<int>(responseJsonDto);
        }
    }
}
