using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModelByDynamic
{
    public class GetListModelByDynamicQuery : IRequest<ModelListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }
    }
    public class GetListModelQueryHandler : IRequestHandler<GetListModelByDynamicQuery, ModelListModel>
    {
        private readonly IModelRepository modelRepository;
        private readonly IMapper mapper;

        public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            this.modelRepository = modelRepository;
            this.mapper = mapper;
        }

        public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await modelRepository.GetListByDynamicAsync(dynamic: request.Dynamic, include: x => x.Include(y => y.Brand), index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            var mappedModels = mapper.Map<ModelListModel>(models);
            return mappedModels;
        }
    }
}
