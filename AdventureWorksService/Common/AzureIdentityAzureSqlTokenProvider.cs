using AdventureWorksService.WebApi.Interfaces;
using Azure.Core;
using Azure.Identity;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Common
{
    // Core implementation that performs token acquisition with Azure Identity
    public class AzureIdentityAzureSqlTokenProvider : IAzureSqlTokenProvider
    {
        private static readonly string[] _azureSqlScopes = new string[]
        {
            "https://database.windows.net//.default"            
        };

        public async Task<(string AccessToken, DateTimeOffset ExpiresOn)> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {            
            var tokenRequestContext = new TokenRequestContext(_azureSqlScopes);            
            var token = await new DefaultAzureCredential().GetTokenAsync(tokenRequestContext, cancellationToken);

            return (token.Token, token.ExpiresOn);
        }

        public (string AccessToken, DateTimeOffset ExpiresOn) GetAccessToken()
        {
            var tokenRequestContext = new TokenRequestContext(_azureSqlScopes);
            var token = new DefaultAzureCredential().GetToken(tokenRequestContext, default);

            return (token.Token, token.ExpiresOn);
        }
    }

}
