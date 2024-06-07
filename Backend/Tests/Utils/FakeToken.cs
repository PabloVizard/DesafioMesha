using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Utils
{
    public static class FakeToken
    {
        public static HttpContext MockHttpContext()
        {

            var expectedAccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjMiLCJOb21lVXN1YXJpbyI6InRlc3RlMiIsIlNlbmhhIjoiZjkxYTc5OWEwMWViYWM5N2M4OTk1ZjUwMzgxYjViZTBkNDNjYTdlYjBjNmE2YzZiYzA3NjAzYzVhOTIyZDFmYSIsIm5iZiI6MTcxNzcyMjA3OCwiZXhwIjoxNzIwNDAwNDc4LCJpYXQiOjE3MTc3MjIwNzh9.okIQKvMYmIOLDqIDpQfu3bfJ80y953ZgxevFrNmlIEc";
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock
                .Setup(x => x.AuthenticateAsync(It.IsAny<HttpContext>(), It.IsAny<string>()))
                .ReturnsAsync(() => new FakeAuthenticationResult(expectedAccessToken));

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock
                .SetupGet(x => x.RequestServices)
                .Returns(() =>
                {
                    var services = new ServiceCollection();
                    services.AddSingleton(authenticationServiceMock.Object);
                    return services.BuildServiceProvider();
                });

            var claims = new List<Claim>
            {
                new Claim("id", "3"),
                new Claim("NomeUsuario", "teste2"),
                new Claim("Senha", Cryptography.ConvertToSha256Hash("teste2")),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");

            httpContextMock.SetupGet(p => p.User).Returns(new ClaimsPrincipal(identity));

            return httpContextMock.Object;
        }

        private class FakeAuthenticationResult : AuthenticateResult
        {
            public FakeAuthenticationResult(string accessToken)
            {
                Properties = new AuthenticationProperties();
                Properties.StoreTokens(new[] { new AuthenticationToken { Name = "access_token", Value = accessToken } });
            }
        }
    }
}
