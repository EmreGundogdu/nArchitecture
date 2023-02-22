using Application.Features.Brands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery : IRequest<BrandListDto>
    {
    }
    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListDto>
    {
        public Task<BrandListDto> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
