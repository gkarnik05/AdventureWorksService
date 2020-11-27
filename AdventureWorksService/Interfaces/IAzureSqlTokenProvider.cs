using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Interfaces
{
    public interface IAzureSqlTokenProvider
    {
        Task<(string AccessToken, DateTimeOffset ExpiresOn)> GetAccessTokenAsync(CancellationToken cancellationToken = default);
        (string AccessToken, DateTimeOffset ExpiresOn) GetAccessToken();
    }
}
