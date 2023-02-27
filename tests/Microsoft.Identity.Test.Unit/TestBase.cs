﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Test.Common;
using Microsoft.Identity.Test.Common.Core.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Identity.Test.Unit
{
    [TestClass]
    public class TestBase
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            EnableFileTracingOnEnvVar();
            Trace.WriteLine("Test run started");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Trace.WriteLine("Test run finished");
            Trace.Flush();
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
#if DESKTOP
            Trace.WriteLine("Framework: .NET FX");
#elif NET6_0
            Trace.WriteLine("Framework: .NET 6");
#elif NET_CORE
            Trace.WriteLine("Framework: .NET Core");
#elif NET6_WIN
            Trace.WriteLine("Framework: .NET6-Win");
#endif
            Trace.WriteLine("Test started " + TestContext.TestName);
            TestCommon.ResetInternalStaticCaches();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Trace.WriteLine("Test finished " + TestContext.TestName);
        }

        public TestContext TestContext { get; set; }

        internal MockHttpAndServiceBundle CreateTestHarness(
            LogCallback logCallback = null,
            bool isExtendedTokenLifetimeEnabled = false,
            bool isMultiCloudSupportEnabled = false,
            bool isInstanceDiscoveryEnabled = true)
        {
            return new MockHttpAndServiceBundle(
                logCallback,
                isExtendedTokenLifetimeEnabled,
                testContext: TestContext,
                isMultiCloudSupportEnabled: isMultiCloudSupportEnabled,
                isInstanceDiscoveryEnabled: isInstanceDiscoveryEnabled
                );
        }

        private static void EnableFileTracingOnEnvVar()
        {
            string traceFile = Environment.GetEnvironmentVariable("MsalTracePath");

            if (!string.IsNullOrEmpty(traceFile))
            {
                Trace.Listeners.Add(new TextWriterTraceListener(traceFile, "testListener"));
            }
        }

        internal MsalTokenResponse CreateMsalRunTimeBrokerTokenResponse(string accessToken = null, string tokenType = null)
        {
            return new MsalTokenResponse()
            {
                AccessToken = accessToken ?? TestConstants.UserAccessToken,
                IdToken = null,
                CorrelationId = null,
                Scope = TestConstants.ScopeStr,
                ExpiresIn = 3600,
                ClientInfo = null,
                TokenType = tokenType ?? "Bearer",
                WamAccountId = TestConstants.LocalAccountId,
                TokenSource = TokenSource.Broker
            };
        }

        internal class IosBrokerMock : NullBroker
        {
            public IosBrokerMock(ILoggerAdapter logger) : base(logger)
            {

            }
            public override bool IsBrokerInstalledAndInvokable(AuthorityType authorityType)
            {
                if (authorityType == AuthorityType.Adfs)
                {
                    return false;
                }

                return true;
            }
        }

        internal static IBroker CreateBroker(Type brokerType)
        {
            if (brokerType == typeof(NullBroker))
            {
                return new NullBroker(null);
            }

            if (brokerType == typeof(IosBrokerMock))
            {
                return new IosBrokerMock(null);
            }

            throw new NotImplementedException();
        }
    }
}
