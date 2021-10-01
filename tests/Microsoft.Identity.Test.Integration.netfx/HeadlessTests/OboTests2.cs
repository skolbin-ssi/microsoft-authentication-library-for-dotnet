﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Json.Utilities;
using Microsoft.Identity.Test.Common;
using Microsoft.Identity.Test.Common.Core.Helpers;
using Microsoft.Identity.Test.Common.Core.Mocks;
using Microsoft.Identity.Test.Integration.Infrastructure;
using Microsoft.Identity.Test.Integration.net45.Infrastructure;
using Microsoft.Identity.Test.LabInfrastructure;
using Microsoft.Identity.Test.Unit;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Microsoft.Identity.Test.Integration.HeadlessTests
{
    [TestClass]
    public class OboTests2
    {
        private static readonly string[] s_scopes = { "User.Read" };
        private static readonly string[] s_publicCloudOBOServiceScope = { "api://23c64cd8-21e4-41dd-9756-ab9e2c23f58c/access_as_user" };
        private static readonly string[] s_arlingtonOBOServiceScope = { "https://arlmsidlab1.us/IDLABS_APP_Confidential_Client/user_impersonation" };

        //TODO: acquire scenario specific client ids from the lab resonse
        private const string PublicCloudPublicClientIDOBO = "be9b0186-7dfd-448a-a944-f771029105bf";
        private const string PublicCloudConfidentialClientIDOBO = "23c64cd8-21e4-41dd-9756-ab9e2c23f58c";
        private const string ArlingtonConfidentialClientIDOBO = "c0555d2d-02f2-4838-802e-3463422e571d";
        private const string ArlingtonPublicClientIDOBO = "cb7faed4-b8c0-49ee-b421-f5ed16894c83";

        //The following client ids are for applications that are within PPE
        private const string OBOClientPpeClientID = "9793041b-9078-4942-b1d2-babdc472cc0c";
        private const string OBOServicePpeClientID = "c84e9c32-0bc9-4a73-af05-9efe9982a322";
        private const string OBOServiceDownStreamApiPpeClientID = "23d08a1e-1249-4f7c-b5a5-cb11f29b6923";
        private const string PPEAuthenticationAuthority = "https://login.windows-ppe.net/f686d426-8d16-42db-81b7-ab578e110ccd";

        private const string PublicCloudHost = "https://login.microsoftonline.com/";
        private const string ArlingtonCloudHost = "https://login.microsoftonline.us/";

        private KeyVaultSecretsProvider _keyVault;

        [TestMethod]
        public async Task ClientCreds_ServicePrincipal_OBO_PPE_Async()
        {
            //An explination of the OBO for service principal scenario can be found here https://aadwiki.windows-int.net/index.php?title=App_OBO_aka._Service_Principal_OBO
            X509Certificate2 cert = GetCertificate();
            IReadOnlyList<string> scopes = new List<string>() { OBOServicePpeClientID + "/.default" };
            IReadOnlyList<string> scopes2 = new List<string>() { OBOServiceDownStreamApiPpeClientID + "/.default" };

            var confidentialSPApp = ConfidentialClientApplicationBuilder
                                    .Create(OBOClientPpeClientID)
                                    .WithAuthority(PPEAuthenticationAuthority)
                                    .WithCertificate(cert)
                                    .WithTestLogging()
                                    .Build();

            var authenticationResult = await confidentialSPApp.AcquireTokenForClient(scopes).ExecuteAsync().ConfigureAwait(false);

            string appToken = authenticationResult.AccessToken;
            var userAssertion = new UserAssertion(appToken);
            string atHash = userAssertion.AssertionHash;

            var _confidentialApp = ConfidentialClientApplicationBuilder
                .Create(OBOServicePpeClientID)
                .WithAuthority(PPEAuthenticationAuthority)
                .WithCertificate(cert)
                .Build();

            var userCacheRecorder = _confidentialApp.UserTokenCache.RecordAccess();

            authenticationResult = await _confidentialApp.AcquireTokenOnBehalfOf(scopes2, userAssertion)
                                                         .WithAuthority(PPEAuthenticationAuthority)
                                                         .ExecuteAsync().ConfigureAwait(false);

            Assert.IsNotNull(authenticationResult);
            Assert.IsNotNull(authenticationResult.AccessToken);
            Assert.AreEqual(TokenSource.IdentityProvider, authenticationResult.AuthenticationResultMetadata.TokenSource);

            authenticationResult = await _confidentialApp.AcquireTokenOnBehalfOf(scopes2, userAssertion)
                                                         .WithAuthority(PPEAuthenticationAuthority)
                                                         .ExecuteAsync().ConfigureAwait(false);

            Assert.IsNotNull(authenticationResult);
            Assert.IsNotNull(authenticationResult.AccessToken);
            Assert.IsTrue(!userCacheRecorder.LastAfterAccessNotificationArgs.IsApplicationCache);
            Assert.IsTrue(userCacheRecorder.LastAfterAccessNotificationArgs.HasTokens);
            Assert.AreEqual(atHash, userCacheRecorder.LastAfterAccessNotificationArgs.SuggestedCacheKey);
            Assert.AreEqual(TokenSource.Cache, authenticationResult.AuthenticationResultMetadata.TokenSource);
        }

        private static X509Certificate2 GetCertificate(bool useRSACert = false)
        {
            X509Certificate2 cert = CertificateHelper.FindCertificateByThumbprint(useRSACert ?
                TestConstants.RSATestCertThumbprint :
                TestConstants.AutomationTestThumbprint);
            if (cert == null)
            {
                throw new InvalidOperationException(
                    "Test setup error - cannot find a certificate in the My store for KeyVault. This is available for Microsoft employees only.");
            }

            return cert;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestCommon.ResetInternalStaticCaches();

            if (_keyVault == null)
            {
                _keyVault = new KeyVaultSecretsProvider();
                // s_publicCloudCcaSecret = _keyVault.GetSecret(TestConstants.MsalCCAKeyVaultUri).Value;
                // s_arlingtonCCASecret = _keyVault.GetSecret(TestConstants.MsalArlingtonCCAKeyVaultUri).Value;
            }
        }


        [TestMethod]
        public async Task WebAPIAccessingGraphOnBehalfOfUserTestAsync()
        {
            await RunOnBehalfOfTestAsync(await LabUserHelper.GetSpecificUserAsync("IDLAB@msidlab4.onmicrosoft.com").ConfigureAwait(false)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory(TestCategories.Arlington)]
        public async Task ArlingtonWebAPIAccessingGraphOnBehalfOfUserTestAsync()
        {
            var labResponse = await LabUserHelper.GetArlingtonUserAsync().ConfigureAwait(false);
            await RunOnBehalfOfTestAsync(labResponse).ConfigureAwait(false);
        }

        [TestCategory(TestCategories.ADFS)]
        public async Task WebAPIAccessingGraphOnBehalfOfADFS2019UserTestAsync()
        {
            await RunOnBehalfOfTestAsync(await LabUserHelper.GetAdfsUserAsync(FederationProvider.ADFSv2019, true).ConfigureAwait(false)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task WebAPIAccessingGraphOnBehalfOfUserWithCacheTestAsync()
        {
            await RunOnBehalfOfTestWithTokenCacheAsync(await LabUserHelper.GetSpecificUserAsync("IDLAB@msidlab4.onmicrosoft.com").ConfigureAwait(false)).ConfigureAwait(false);
        }

        //Since this test performs a large number of operations it should not be rerun on other clouds.
        private async Task RunOnBehalfOfTestWithTokenCacheAsync(LabResponse labResponse)
        {
            LabUser user = labResponse.User;
            string oboHost;
            string secret;
            string authority;
            string publicClientID;
            string confidentialClientID;
            string[] oboScope;

            oboHost = PublicCloudHost;
            secret = _keyVault.GetSecret(TestConstants.MsalOBOKeyVaultUri).Value;
            authority = TestConstants.AuthorityOrganizationsTenant;
            publicClientID = PublicCloudPublicClientIDOBO;
            confidentialClientID = PublicCloudConfidentialClientIDOBO;
            oboScope = s_publicCloudOBOServiceScope;

            //TODO: acquire scenario specific client ids from the lab response

            SecureString securePassword = new NetworkCredential("", user.GetOrFetchPassword()).SecurePassword;
            var factory = new HttpSnifferClientFactory();

            var msalPublicClient = PublicClientApplicationBuilder.Create(publicClientID)
                                                                 .WithAuthority(authority)
                                                                 .WithRedirectUri(TestConstants.RedirectUri)
                                                                 .WithTestLogging()
                                                                 .WithHttpClientFactory(factory)
                                                                 .Build();

            var builder = msalPublicClient.AcquireTokenByUsernamePassword(oboScope, user.Upn, securePassword);

            builder.WithAuthority(authority);

            var authResult = await builder.ExecuteAsync().ConfigureAwait(false);

            var confidentialApp = ConfidentialClientApplicationBuilder
                .Create(confidentialClientID)
                .WithAuthority(new Uri(oboHost + authResult.TenantId), true)
                .WithClientSecret(secret)
                .WithTestLogging()
                .BuildConcrete();

            var userCacheRecorder = confidentialApp.UserTokenCache.RecordAccess();

            UserAssertion userAssertion = new UserAssertion(authResult.AccessToken);

            string atHash = userAssertion.AssertionHash;

            authResult = await confidentialApp.AcquireTokenOnBehalfOf(s_scopes, userAssertion)
                .ExecuteAsync(CancellationToken.None)
                .ConfigureAwait(false);

            MsalAssert.AssertAuthResult(authResult, user);
            Assert.AreEqual(atHash, userCacheRecorder.LastAfterAccessNotificationArgs.SuggestedCacheKey);

            //Run OBO again. Should get token from cache
            authResult = await confidentialApp.AcquireTokenOnBehalfOf(s_scopes, userAssertion)
                .ExecuteAsync(CancellationToken.None)
                .ConfigureAwait(false);


            Assert.IsNotNull(authResult);
            Assert.IsNotNull(authResult.AccessToken);
            Assert.IsNotNull(authResult.IdToken);
            Assert.IsTrue(!userCacheRecorder.LastAfterAccessNotificationArgs.IsApplicationCache);
            Assert.IsTrue(userCacheRecorder.LastAfterAccessNotificationArgs.HasTokens);
            Assert.AreEqual(atHash, userCacheRecorder.LastAfterAccessNotificationArgs.SuggestedCacheKey);
            Assert.AreEqual(TokenSource.Cache, authResult.AuthenticationResultMetadata.TokenSource);

            //Expire access tokens
            TokenCacheHelper.ExpireAllAccessTokens(confidentialApp.UserTokenCacheInternal);

            //Run OBO again. Should do token refresh since the AT is expired
            authResult = await confidentialApp.AcquireTokenOnBehalfOf(s_scopes, userAssertion)
                .ExecuteAsync(CancellationToken.None)
                .ConfigureAwait(false);

            Assert.IsNotNull(authResult);
            Assert.IsNotNull(authResult.AccessToken);
            Assert.IsNotNull(authResult.IdToken);
            Assert.IsTrue(!userCacheRecorder.LastAfterAccessNotificationArgs.IsApplicationCache);
            Assert.IsTrue(userCacheRecorder.LastAfterAccessNotificationArgs.HasTokens);
            Assert.AreEqual(atHash, userCacheRecorder.LastAfterAccessNotificationArgs.SuggestedCacheKey);
            Assert.AreEqual(TokenSource.IdentityProvider, authResult.AuthenticationResultMetadata.TokenSource);
            AssertLastHttpContent("refresh_token");

            //creating second app with no refresh tokens
            var atItems = confidentialApp.UserTokenCacheInternal.Accessor.GetAllAccessTokens();
            var confidentialApp2 = ConfidentialClientApplicationBuilder
                .Create(confidentialClientID)
                .WithAuthority(new Uri(oboHost + authResult.TenantId), true)
                .WithClientSecret(secret)
                .WithTestLogging()
                .WithHttpClientFactory(factory)
                .BuildConcrete();

            TokenCacheHelper.ExpireAccessToken(confidentialApp2.UserTokenCacheInternal, atItems.FirstOrDefault());

            //Should perform OBO flow since the access token is expired and the refresh token does not exist
            authResult = await confidentialApp2.AcquireTokenOnBehalfOf(s_scopes, userAssertion)
                .ExecuteAsync(CancellationToken.None)
                .ConfigureAwait(false);

            Assert.IsNotNull(authResult);
            Assert.IsNotNull(authResult.AccessToken);
            Assert.IsNotNull(authResult.IdToken);
            Assert.IsTrue(!userCacheRecorder.LastAfterAccessNotificationArgs.IsApplicationCache);
            Assert.IsTrue(userCacheRecorder.LastAfterAccessNotificationArgs.HasTokens);
            Assert.AreEqual(atHash, userCacheRecorder.LastAfterAccessNotificationArgs.SuggestedCacheKey);
            Assert.AreEqual(TokenSource.IdentityProvider, authResult.AuthenticationResultMetadata.TokenSource);
            AssertLastHttpContent("on_behalf_of");

            TokenCacheHelper.ExpireAllAccessTokens(confidentialApp2.UserTokenCacheInternal);
            TokenCacheHelper.UpdateUserAssertions(confidentialApp2);

            //Should perform OBO flow since the access token and the refresh token contains the wrong user assertion hash
            authResult = await confidentialApp2.AcquireTokenOnBehalfOf(s_scopes, userAssertion)
                .ExecuteAsync(CancellationToken.None)
                .ConfigureAwait(false);

            Assert.IsNotNull(authResult);
            Assert.IsNotNull(authResult.AccessToken);
            Assert.IsNotNull(authResult.IdToken);
            Assert.IsTrue(!userCacheRecorder.LastAfterAccessNotificationArgs.IsApplicationCache);
            Assert.IsTrue(userCacheRecorder.LastAfterAccessNotificationArgs.HasTokens);
            Assert.AreEqual(atHash, userCacheRecorder.LastAfterAccessNotificationArgs.SuggestedCacheKey);
            Assert.AreEqual(TokenSource.IdentityProvider, authResult.AuthenticationResultMetadata.TokenSource);
            AssertLastHttpContent("on_behalf_of");
        }

        private void AssertLastHttpContent(string content)
        {
            Assert.IsTrue(HttpSnifferClientFactory.LastHttpContentData.Contains(content));
            HttpSnifferClientFactory.LastHttpContentData = string.Empty;
        }

        private async Task RunOnBehalfOfTestAsync(LabResponse labResponse)
        {
            LabUser user = labResponse.User;
            string oboHost;
            string secret;
            string authority;
            string publicClientID;
            string confidentialClientID;
            string[] oboScope;

            switch (labResponse.User.AzureEnvironment)
            {
                case AzureEnvironment.azureusgovernment:
                    oboHost = ArlingtonCloudHost;
                    secret = _keyVault.GetSecret(TestConstants.MsalArlingtonOBOKeyVaultUri).Value;
                    authority = labResponse.Lab.Authority + "organizations";
                    publicClientID = ArlingtonPublicClientIDOBO;
                    confidentialClientID = ArlingtonConfidentialClientIDOBO;
                    oboScope = s_arlingtonOBOServiceScope;
                    break;
                default:
                    oboHost = PublicCloudHost;
                    secret = _keyVault.GetSecret(TestConstants.MsalOBOKeyVaultUri).Value;
                    authority = TestConstants.AuthorityOrganizationsTenant;
                    publicClientID = PublicCloudPublicClientIDOBO;
                    confidentialClientID = PublicCloudConfidentialClientIDOBO;
                    oboScope = s_publicCloudOBOServiceScope;
                    break;
            }

            //TODO: acquire scenario specific client ids from the lab response

            SecureString securePassword = new NetworkCredential("", user.GetOrFetchPassword()).SecurePassword;

            var msalPublicClient = PublicClientApplicationBuilder.Create(publicClientID)
                                                                 .WithAuthority(authority)
                                                                 .WithRedirectUri(TestConstants.RedirectUri)
                                                                 .WithTestLogging()
                                                                 .Build();

            var builder = msalPublicClient.AcquireTokenByUsernamePassword(oboScope, user.Upn, securePassword);

            builder.WithAuthority(authority);

            var authResult = await builder.ExecuteAsync().ConfigureAwait(false);

            var confidentialApp = ConfidentialClientApplicationBuilder
                .Create(confidentialClientID)
                .WithAuthority(new Uri(oboHost + authResult.TenantId), true)
                .WithClientSecret(secret)
                .WithTestLogging()
                .Build();

            var userCacheRecorder = confidentialApp.UserTokenCache.RecordAccess();

            UserAssertion userAssertion = new UserAssertion(authResult.AccessToken);

            string atHash = userAssertion.AssertionHash;

            authResult = await confidentialApp.AcquireTokenOnBehalfOf(s_scopes, userAssertion)
                .ExecuteAsync(CancellationToken.None)
                .ConfigureAwait(false);

            MsalAssert.AssertAuthResult(authResult, user);
            Assert.AreEqual(atHash, userCacheRecorder.LastAfterAccessNotificationArgs.SuggestedCacheKey);

#pragma warning disable CS0618 // Type or member is obsolete
            await confidentialApp.GetAccountsAsync().ConfigureAwait(false);
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.IsNull(userCacheRecorder.LastAfterAccessNotificationArgs.SuggestedCacheKey);
        }

    }
}
