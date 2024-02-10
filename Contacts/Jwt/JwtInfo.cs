using System.IdentityModel.Tokens.Jwt;

namespace Contacts.Jwt
{
    public class JwtInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public JwtInfo(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetName()
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                var token = authorizationHeader.ToString().Replace("Bearer ", "");

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var nameClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");

                    if (nameClaim != null)
                    {
                        return nameClaim.Value;
                    }
                }
            }

            return null;
        }
    }
}
