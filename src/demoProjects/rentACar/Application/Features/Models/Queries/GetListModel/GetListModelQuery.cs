using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListModel
{
    public class GetListModelQuery : IRequest<ModelListModel>
    {
        public PageRequest PageRequest { get; set; }
    }
    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>
    {
        private readonly IModelRepository modelRepository;
        private readonly IMapper mapper;

        public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            this.modelRepository = modelRepository;
            this.mapper = mapper;
        }

        public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await modelRepository.GetListAsync(include: x => x.Include(y => y.Brand), index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            var mappedModels = mapper.Map<ModelListModel>(models);
            return mappedModels;
        }
    }
}
