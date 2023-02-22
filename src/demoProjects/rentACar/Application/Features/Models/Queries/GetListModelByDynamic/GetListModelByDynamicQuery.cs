using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Models.Queries.GetListModelByDynamic
{
    public class GetListModelByDynamicQuery : IRequest<ModelListModel>
    {
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
            IPaginate<Model> models = await modelRepository.GetListAsync(include: x => x.Include(y => y.Brand), index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            var mappedModels = mapper.Map<ModelListModel>(models);
            return mappedModels;
        }
    }
}
