using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }
    }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
    {
        private readonly AuthBusinessRules authBusinessRules;
        readonly IUserRepository userRepository;

        public RegisterCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository)
        {
            this.authBusinessRules = authBusinessRules;
            this.userRepository = userRepository;
        }

        public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);
        }
    }
}
