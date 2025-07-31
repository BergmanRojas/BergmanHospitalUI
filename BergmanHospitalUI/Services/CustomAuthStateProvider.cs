using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace BergmanHospitalUI.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly TokenProvider _tokenProvider;

        public CustomAuthStateProvider(TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }
    public async Task MarkUserAsAuthenticated(string token)
        {
            _tokenProvider.SetToken(token);

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return token.Claims;
        }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await _tokenProvider.LoadTokenAsync();

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrWhiteSpace(_tokenProvider.Token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(_tokenProvider.Token);

                identity = new ClaimsIdentity(jwt.Claims, "jwt");
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }



        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
