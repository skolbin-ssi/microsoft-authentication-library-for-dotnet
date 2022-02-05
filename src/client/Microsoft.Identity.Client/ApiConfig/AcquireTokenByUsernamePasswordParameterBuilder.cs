﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
    /// <summary>
    /// Parameter builder for the <see cref="IPublicClientApplication.AcquireTokenByUsernamePassword(IEnumerable{string}, string, SecureString)"/>
    /// operation. See https://aka.ms/msal-net-up
    /// </summary>
    public sealed class AcquireTokenByUsernamePasswordParameterBuilder :
        AbstractPublicClientAcquireTokenParameterBuilder<AcquireTokenByUsernamePasswordParameterBuilder>
    {
        private AcquireTokenByUsernamePasswordParameters Parameters { get; } = new AcquireTokenByUsernamePasswordParameters();

        internal AcquireTokenByUsernamePasswordParameterBuilder(IPublicClientApplicationExecutor publicClientApplicationExecutor)
            : base(publicClientApplicationExecutor)
        {
        }

        internal static AcquireTokenByUsernamePasswordParameterBuilder Create(
            IPublicClientApplicationExecutor publicClientApplicationExecutor,
            IEnumerable<string> scopes,
            string username,
            SecureString password)
        {
            return new AcquireTokenByUsernamePasswordParameterBuilder(publicClientApplicationExecutor)
                   .WithScopes(scopes).WithUsername(username).WithPassword(password);
        }

        /// <summary>
        /// Enables MSAL to read the federation metadata for a WS-Trust exchange from the provided input instead of acquiring it from an endpoint.
        /// This is only applicable for managed ADFS accounts. See https://aka.ms/MsalFederationMetadata.
        /// </summary>
        /// <param name="federationMetadata">Federation metadata in the form of XML.</param>
        /// <returns>The builder to chain the .With methods</returns>
        public AcquireTokenByUsernamePasswordParameterBuilder WithFederationMetadata(string federationMetadata)
        {
            Parameters.FederationMetadata = federationMetadata;
            return this;
        }

        private AcquireTokenByUsernamePasswordParameterBuilder WithUsername(string username)
        {
            Parameters.Username = username;
            return this;
        }

        private AcquireTokenByUsernamePasswordParameterBuilder WithPassword(SecureString password)
        {
            Parameters.Password = password;
            return this;
        }

        /// <inheritdoc />
        internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
        {
            return PublicClientApplicationExecutor.ExecuteAsync(CommonParameters, Parameters, cancellationToken);
        }

        /// <inheritdoc />
        internal override ApiEvent.ApiIds CalculateApiEventId()
        {
            return ApiEvent.ApiIds.AcquireTokenByUsernamePassword;
        }
    }
}
