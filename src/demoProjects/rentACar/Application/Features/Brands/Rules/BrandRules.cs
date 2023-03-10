using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.someFeature.Rules
{
    public class BrandRules
    {
        private readonly IBrandRepository brandRepository;

        public BrandRules(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Brand> result = await brandRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Brand name exists.");
        }

        public async Task BrandShouldExistWhenRequested(int id)
        {
            Brand result = await brandRepository.GetAsync(b => b.Id == id);
            if (result == null) throw new BusinessException("Requested brand not exists.");
        }
    }
}
