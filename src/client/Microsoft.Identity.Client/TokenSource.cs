﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Identity.Client
{
    /// <summary>
    /// Specifies the source of the access and Id tokens in the authentication result.
    /// </summary>
    public enum TokenSource
    {
        /// <summary>
        /// The source of the access and Id token is Identity Provider - Microsoft Entra ID, ADFS or AAD B2C.
        /// </summary>
        IdentityProvider,
        /// <summary>
        /// The source of access and Id token is MSAL's cache.
        /// </summary>
        Cache,
        /// <summary>
        /// The source of the access and Id token is a broker application - Authenticator or Company Portal. Brokers are supported only on Android and iOS.
        /// </summary>
        Broker
    }
}
