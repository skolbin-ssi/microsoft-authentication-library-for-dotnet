﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#if HAVE_METHOD_IMPL_ATTRIBUTE
using System.Runtime.CompilerServices;
#endif

namespace Microsoft.Identity.Client.Utils
{
    internal static class CollectionHelpers
    {
#if HAVE_METHOD_IMPL_ATTRIBUTE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static IReadOnlyList<T> GetEmptyReadOnlyList<T>()
        {
#if !NET45
            return Array.Empty<T>();
#else
            return new List<T>();
#endif
        }

        public static IDictionary<TKey, TValue> GetEmptyDictionary<TKey, TValue>()
        {
#if NET_CORE
            return System.Collections.Immutable.ImmutableDictionary<TKey, TValue>.Empty;
#else
            return new Dictionary<TKey, TValue>();
#endif
        }
    }
}
