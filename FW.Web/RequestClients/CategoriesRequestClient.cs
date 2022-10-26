using AutoMapper;
using FW.BusinessLogic.Contracts;
using FW.BusinessLogic.Contracts.Category;
using FW.Web.RequestClients.Interfaces;
using FW.Web.ViewModels.Categories;
using MassTransit;

namespace FW.Web.RequestClients
{
    public class CategoriesRequestClient : ICategoriesRequestClient
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRequestClient<CategoryCreateDto> _createClient;
        private readonly IRequestClient<CategoryGetByIdDto> _getByIdClient;
        private readonly IRequestClient<CategoryUpdateDto> _updateClient;
        private readonly IRequestClient<CategoryDeleteDto> _deleteClient;
        private readonly IRequestClient<CategoriesGetPageDto> _getPageClient;
        private readonly IRequestClient<CategoriesGetAllDto> _getAllClient;
        private readonly IRequestClient<CategoriesGetCountDto> _getCountClient;

        public CategoriesRequestClient(
            IMapper mapper,
            ILogger logger,
            IRequestClient<CategoryCreateDto> createClient,
            IRequestClient<CategoryGetByIdDto> getByIdClient,
            IRequestClient<CategoryUpdateDto> updateClient,
            IRequestClient<CategoryDeleteDto> deleteClient,
            IRequestClient<CategoriesGetPageDto> getPageClient,
            IRequestClient<CategoriesGetAllDto> getAllClient,
            IRequestClient<CategoriesGetCountDto> getCountClient)
        {
            _mapper = mapper;
            _logger = logger;
            _getByIdClient = getByIdClient;
            _getPageClient = getPageClient;
            _createClient = createClient;
            _updateClient = updateClient;
            _deleteClient = deleteClient;
            _getAllClient = getAllClient;
            _getCountClient = getCountClient;
        }

        public async Task<CategoryResponseVM> Get(Guid id, Guid userId)
        {
            var queryDto = new CategoryGetByIdDto { Id = id, UserId = userId };

            var responseDto = await _getByIdClient.GetResponse<CategoryResponseDto>(queryDto);

            var result = _mapper.Map<CategoryResponseVM>(responseDto.Message);
            return result;
        }

        public async Task<List<CategoryResponseVM>> GetPage(UInt16 pageNumber, UInt16 pageSize, Guid userId)
        {
            var queryDto = new CategoriesGetPageDto { PageNumber = pageNumber, PageSize = pageSize, UserId = userId };

            var responseDto = await _getPageClient.GetResponse<CategoriesResponseDto>(queryDto);

            var result = _mapper.Map<List<CategoryResponseVM>>(responseDto.Message.Categories);
            return result;
        }

        public async Task<List<CategoryResponseVM>> GetAll(Guid userId)
        {
            var queryDto = new CategoriesGetAllDto{ UserId = userId };

            var responseDto = await _getAllClient.GetResponse<CategoriesResponseDto>(queryDto);

            var result = _mapper.Map<List<CategoryResponseVM>>(responseDto.Message.Categories);
            return result;
        }

        public async Task<int> Count(Guid userId)
        {
            var queryDto = new CategoriesGetCountDto{ UserId = userId };

            var responseDto = await _getCountClient.GetResponse<CountResponseDto>(queryDto);

            var result = responseDto.Message;
            return result.Count;
        }

        public async Task<ResponseStatusResult> Create(CategoryVM category, Guid userId)
        {
            var queryDto = _mapper.Map<CategoryCreateDto>(category);
            queryDto.UserId = userId;

            var responseDto = await _createClient.GetResponse<ResponseStatusResult>(queryDto);

            var result = responseDto.Message;
            return result;
        }

        public async Task<ResponseStatusResult> Update(Guid id, CategoryVM category, Guid userId)
        {
            var queryDto = _mapper.Map<CategoryUpdateDto>(category);
            queryDto.Id = id;
            queryDto.UserId = userId;

            var responseDto = await _updateClient.GetResponse<ResponseStatusResult>(queryDto);

            var result = responseDto.Message;
            return result;
        }

        public async Task<ResponseStatusResult> Delete(Guid id, Guid userId)
        {
            var queryDto = new CategoryDeleteDto { Id = id, UserId = userId };

            var responseDto = await _deleteClient.GetResponse<ResponseStatusResult>(queryDto);

            var result = responseDto.Message;
            return result;
        }
    }
}
