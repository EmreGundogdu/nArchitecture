using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListModel
{
    public class GetListModelQuery:IRequest<ModelListModel>
    {
        public PageRequest PageRequest{ get; set; }
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

        public Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
