using Application.Features.Brands.Dtos;
using Application.Features.someFeature.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Queries.GetBrandById
{
    public class GetBrandByIdQuery : IRequest<BrandDto>
    {
        public int Id { get; set; }
    }
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
    {
        readonly IBrandRepository brandRepository;
        readonly IMapper mapper;
        readonly BrandRules brandRules;

        public GetBrandByIdQueryHandler(IBrandRepository brandRepository, IMapper mapper, BrandRules brandRules)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
            this.brandRules = brandRules;
        }

        public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            await brandRules.BrandShouldExistWhenRequested(request.Id);
            var entity = await brandRepository.GetAsync(x => x.Id == request.Id);
            var mappedBrand = mapper.Map<BrandDto>(entity);
            return mappedBrand;
        }
    }
}
