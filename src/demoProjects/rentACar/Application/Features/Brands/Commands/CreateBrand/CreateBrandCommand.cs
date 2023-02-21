using Application.Features.Brands.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }
    }
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var mappedBrand = _mapper.Map<Brand>(request);
            var createdBrand = await _brandRepository.AddAsync(mappedBrand);
            var createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);
            return createdBrandDto;
        }
    }
}
