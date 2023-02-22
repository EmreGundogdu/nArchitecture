using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserOperationClaimRepository userOperationClaimRepository;
        private readonly ITokenHelper tokenHelper;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public AuthService(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            this.userOperationClaimRepository = userOperationClaimRepository;
            this.tokenHelper = tokenHelper;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            var token = await refreshTokenRepository.AddAsync(refreshToken);
            return token;    
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IPaginate<UserOperationClaim> userClaims = await userOperationClaimRepository.GetListAsync(x => x.UserId == user.Id, include: y => y.Include(z => z.OperationClaim));
            IList<OperationClaim> operationClaims = userClaims.Items.Select(x => new OperationClaim { Id = x.OperationClaim.Id, Name = x.OperationClaim.Name }).ToList();
            AccessToken accessToken = tokenHelper.CreateToken(user,operationClaims);
            return accessToken;
        }

        public async Task<RefreshToken> CreateRefreshToken(User user,string ipAddress)
        {
            var token = tokenHelper.CreateRefreshToken(user,ipAddress);
            return await Task.FromResult(token);
        }
    }
}
