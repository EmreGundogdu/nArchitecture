﻿using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
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
        public PageRequest PageRequest { get; set; }
    }
    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListDto>
    {
        IBrandRepository brandRepository;
        IMapper mapper;

        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        public async Task<BrandListDto> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await brandRepository.GetListAsync(index:request.PageRequest.Page,size: request.PageRequest.PageSize);
            BrandListModel mappedBrandList = mapper.Map<BrandListModel>(brands);
            return mappedBrandList;
        }
    }
}