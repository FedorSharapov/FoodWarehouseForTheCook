using AutoMapper;
using FW.BusinessLogic.Contracts.ChangesProducts;
using FW.BusinessLogic.Services.Abstractions;
using FW.Domain;
using FW.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FW.BusinessLogic.Services
{
    public class ChangesProductsService : IChangesProductsService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public ChangesProductsService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ChangesProductResponseDto>> GetAll(Guid userId)
        {
            var changesProducts = await _dbContext.ChangesProducts
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<ChangesProductResponseDto>>(changesProducts);
        }

        public async Task<List<ChangesProductResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId)
        {
            var changesProducts = await _dbContext.ChangesProducts
                .Where(p => p.UserId == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<List<ChangesProductResponseDto>>(changesProducts);
        }

        public async Task<ChangesProductResponseDto> GetById(Guid id, Guid userId)
        {
            var changesProduct = await FirstOrDefaultAsync(id, userId);
            return _mapper.Map<ChangesProductResponseDto>(changesProduct);
        }

        public async Task<int> Count(Guid userId)
        {
            var count = await _dbContext.ChangesProducts
                .Where(p => p.UserId == userId)
                .CountAsync();
            return count;
        }

        private async Task<ChangesProducts?> FirstOrDefaultAsync(Guid id, Guid userId)
        {
            return await _dbContext.ChangesProducts
                .Where(p => p.Id == id && p.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
