using AdventureWorksService.WebApi.Interfaces;
using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi.Common
{
    public class AadAuthenticationDbConnectionInterceptor : DbConnectionInterceptor
    {
        private readonly IAzureSqlTokenProvider _tokenProvider;
        private readonly ILogger<AadAuthenticationDbConnectionInterceptor> _logger;

        public AadAuthenticationDbConnectionInterceptor(IAzureSqlTokenProvider tokenProvider, ILogger<AadAuthenticationDbConnectionInterceptor> logger)
        {
            _tokenProvider = tokenProvider;
            _logger = logger;
        }

        public override InterceptionResult ConnectionOpening(
            DbConnection connection,
            ConnectionEventData eventData,
            InterceptionResult result)
        {
            var sqlConnection = (SqlConnection)connection;

            if (ConnectionNeedsAccessToken(sqlConnection))
            {
                var (token, _) = _tokenProvider.GetAccessToken();
                sqlConnection.AccessToken = token;

                _logger.LogInformation("{@AccessToken}", token);
            }

            return base.ConnectionOpening(connection, eventData, result);
        }

        public override async Task<InterceptionResult> ConnectionOpeningAsync(
            DbConnection connection,
            ConnectionEventData eventData,
            InterceptionResult result,
            CancellationToken cancellationToken = default)
        {
            var sqlConnection = (SqlConnection)connection;

            if (ConnectionNeedsAccessToken(sqlConnection))
            {
                var (token, _) = await _tokenProvider.GetAccessTokenAsync(cancellationToken);
                sqlConnection.AccessToken = token;

                _logger.LogInformation("{@AccessToken}", token);
            }

            return await base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
        }

        private static bool ConnectionNeedsAccessToken(SqlConnection connection)
        {
            //
            // Only try to get a token from AAD if
            //  - We connect to an Azure SQL instance; and
            //  - The connection doesn't specify a username.
            //
            var connectionStringBuilder = new SqlConnectionStringBuilder(connection.ConnectionString);

            return connectionStringBuilder.DataSource.Contains("database.windows.net", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(connectionStringBuilder.UserID);
        }
    }
}
