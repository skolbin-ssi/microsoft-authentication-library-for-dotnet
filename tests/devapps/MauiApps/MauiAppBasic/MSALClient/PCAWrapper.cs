﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace MauiAppBasic.MSALClient
{
    /// <summary>
    /// This is a wrapper for PCA. It is singleton and can be utilized by both application and the MAM callback
    /// </summary>
    public class PCAWrapper
    {
        /// <summary>
        /// This is the singleton used by consumers
        /// </summary>
        static public PCAWrapper Instance { get; } = new PCAWrapper();

        internal IPublicClientApplication PCA { get; }

        internal bool UseEmbedded { get; set; } = false;

        /// <summary>
        /// The authority for the MSAL PublicClientApplication. Sign in will use this URL.
        /// </summary>
        private const string _authority = "https://login.microsoftonline.com/common";

        // ClientID of the application in (msidlab20.com)
        internal const string ClientId = "15968a65-46b5-4cb2-886e-94e7589cc3b1"; // TODO - Replace with your client Id. And also replace in the AndroidManifest.xml

        public static string[] Scopes = { "User.Read" };

        // private constructor for singleton
        private PCAWrapper()
        {
            // Create PCA once. Make sure that all the config parameters below are passed
            PCA = PublicClientApplicationBuilder
                                        .Create(ClientId)
                                        .WithRedirectUri(PlatformConfigImpl.Instance.RedirectUri)
                                        .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
                                        .Build();
        }

        /// <summary>
        /// Perform the intractive acquistion of the token for the given scope
        /// </summary>
        /// <param name="scopes">desired scopes</param>
        /// <returns></returns>
        internal async Task<AuthenticationResult> AcquireTokenInteractiveAsync(string[] scopes)
        {
#if IOS
            // embedded view is not supported on Android
            if (UseEmbedded)
            {

                return await PCA.AcquireTokenInteractive(scopes)
                                        .WithUseEmbeddedWebView(true)
                                        .WithParentActivityOrWindow(PlatformConfigImpl.Instance.ParentWindow)
                                        .ExecuteAsync()
                                        .ConfigureAwait(false);
            }
#endif
            // Hide the privacy prompt
            SystemWebViewOptions systemWebViewOptions = new SystemWebViewOptions()
            {
                iOSHidePrivacyPrompt = true,
            };

            return await PCA.AcquireTokenInteractive(scopes)
                                    .WithSystemWebViewOptions(systemWebViewOptions)
                                    .WithParentActivityOrWindow(PlatformConfigImpl.Instance.ParentWindow)
                                    .ExecuteAsync()
                                    .ConfigureAwait(false);
        }

        /// <summary>
        /// Acquire the token silently
        /// </summary>
        /// <param name="scopes">desired scopes</param>
        /// <returns>Authenticaiton result</returns>
        public async Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes)
        {
            var accts = await PCA.GetAccountsAsync().ConfigureAwait(false);
            var acct = accts.FirstOrDefault();

            var authResult = await PCA.AcquireTokenSilent(scopes, acct)
                                        .ExecuteAsync().ConfigureAwait(false);
            return authResult;

        }

        /// <summary>
        /// Signout may not perform the complete signout as company portal may hold
        /// the token.
        /// </summary>
        /// <returns></returns>
        internal async Task SignOut()
        {
            var accounts = await PCA.GetAccountsAsync().ConfigureAwait(false);
            foreach (var acct in accounts)
            {
                await PCA.RemoveAsync(acct).ConfigureAwait(false);
            }
        }
    }
}
