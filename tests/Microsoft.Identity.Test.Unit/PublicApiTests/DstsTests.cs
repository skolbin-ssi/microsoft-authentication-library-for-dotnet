﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.UI;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Test.Common.Core.Helpers;
using Microsoft.Identity.Test.Common.Core.Mocks;
using Microsoft.Identity.Test.Common.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Identity.Test.Unit.PublicApiTests
{
    [TestClass]
    public class DstsTests : TestBase
    {
        private const string CommonAuthority = "https://foo.bar.dsts.core.azure-test.net/dstsv2/common";
        private const string TenantedAuthority = "https://foo.bar.dsts.core.azure-test.net/dstsv2/tenantId";

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        private static MockHttpMessageHandler CreateTokenResponseHttpHandler(string authority)
        {
            IDictionary<string, string> expectedRequestBody = new Dictionary<string, string>();
            expectedRequestBody.Add("scope", TestConstants.ScopeStr);
            expectedRequestBody.Add("grant_type", "client_credentials");
            expectedRequestBody.Add("client_id", TestConstants.ClientId);
            expectedRequestBody.Add("client_secret", TestConstants.ClientSecret);

            return new MockHttpMessageHandler()
            {
                ExpectedUrl = $"{authority}/dstsv2/token",
                ExpectedMethod = HttpMethod.Post,
                ExpectedPostData = expectedRequestBody,
                ResponseMessage = MockHelpers.CreateSuccessfulClientCredentialTokenResponseMessage(MockHelpers.CreateClientInfo(TestConstants.Uid, TestConstants.Utid))
            };
        }

        [DataTestMethod]
        [DataRow("common")]
        [DataRow("tenantid")]
        public async Task DstsClientCredentialSuccessfulTestAsync(string tenantId)
        {
            string authority = $"https://foo.bar.test.core.azure-test.net/dstsv2/{tenantId}";

            using (var httpManager = new MockHttpManager())
            {
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                    .Create(TestConstants.ClientId)
                    .WithHttpManager(httpManager)
                    .WithAuthority(authority)
                    .WithClientSecret(TestConstants.ClientSecret)
                    .Build();

                Assert.AreEqual(authority + "/", app.Authority);
                var confidentailClientApp = (ConfidentialClientApplication)app;
                Assert.AreEqual(AuthorityType.Dsts, confidentailClientApp.AuthorityInfo.AuthorityType);

                httpManager.AddMockHandler(CreateTokenResponseHttpHandler(authority));

                AuthenticationResult result = await app
                    .AcquireTokenForClient(TestConstants.s_scope)
                    .ExecuteAsync(CancellationToken.None)
                    .ConfigureAwait(false);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.AccessToken);
                Assert.AreEqual(TokenSource.IdentityProvider, result.AuthenticationResultMetadata.TokenSource);

                result = await app
                    .AcquireTokenForClient(TestConstants.s_scope)
                    .ExecuteAsync(CancellationToken.None)
                    .ConfigureAwait(false);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.AccessToken);
                Assert.AreEqual(TokenSource.Cache, result.AuthenticationResultMetadata.TokenSource);    
            }
        }
    }
}
