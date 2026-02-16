using ACT_Hotelaria.Application.Abstract.Authentication;
using Microsoft.AspNetCore.Http;

namespace ACT_Hotelaria.Auth.Authentication;

public class TokenProviderService : ITokenProvider
{
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProviderService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetToken()
        {
            var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            return authentication["Bearer ".Length..].Trim();
        }
        
}