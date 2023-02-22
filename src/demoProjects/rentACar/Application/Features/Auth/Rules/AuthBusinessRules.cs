using Application.Services.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await userRepository.GetAsync(x=>x.Email==email);
            if (user != null)
            {
                throw new Exception("Mail already exists!");
            }
        }
    }
}
