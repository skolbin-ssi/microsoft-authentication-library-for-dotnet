﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Microsoft.Identity.Client
{
    /// <summary>
    /// Extension methods for <see cref="IAccount"/>
    /// </summary>
    public static class AccountExtensions
    {
        /// <summary>        
        /// The same account can exist in its home tenant and also as a guest in multiple other tenants. 
        /// <see cref="TenantProfile"/> is derived from the ID token for that tenant.
        /// </summary>
        /// <remarks>Only tenants for which a token was acquired will be available in <see cref="Account.TenantProfiles"/> property.</remarks>
        public static IEnumerable<TenantProfile> GetTenantProfiles(this IAccount account)
        {
            return (account as Account)?.TenantProfiles;
        }
    }
}
