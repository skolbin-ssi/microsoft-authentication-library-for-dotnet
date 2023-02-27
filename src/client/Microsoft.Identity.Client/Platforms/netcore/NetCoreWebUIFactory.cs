﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser;
using Microsoft.Identity.Client.PlatformsCommon.Shared;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Platforms.Shared.NetStdCore
{
    internal class NetCoreWebUIFactory : IWebUIFactory
    {
        public bool IsSystemWebViewAvailable => IsUserInteractive;

        public bool IsUserInteractive => DesktopOsHelper.IsUserInteractive();

        public bool IsEmbeddedWebViewAvailable => false;

        public IWebUI CreateAuthenticationDialog(
            CoreUIParent coreUIParent,
            WebViewPreference webViewPreference,
            RequestContext requestContext)
        {
            if (webViewPreference == WebViewPreference.Embedded)
            {
                throw new MsalClientException(MsalError.WebviewUnavailable,
                    "If you have a Windows application which targets net5.0, net5.0-windows, net6.0, or net6.0-windows, change the target to at least net6.0-windows10.0.17763.0, " +
                        "which provides support from Windows 7 to Windows 10. For details, see https://github.com/dotnet/designs/blob/main/accepted/2020/platform-checks/platform-checks.md" +

                    "If you have a cross-platform (Windows, Mac, Linux) application which targets net6.0, dual target net6.0 and net6.0-windows10.0.17763.0." +
                        "Your installer should deploy the net6.0 version on Mac and Linux and the net6.0-window10.0.17763.0 on Windows." +
                        "For details, see https://github.com/dotnet/designs/blob/main/accepted/2020/platform-checks/platform-checks.md" +

                    "If you have a .NET Core 3.1 app, reference the NuGet package Microsoft.Identity.Client.Desktop and call the extension method .WithDesktopFeatures() first." + "For details, see https://aka.ms/msal-net-webview2 or use the system WebView - see https://aka.ms/msal-net-os-browser");
            }

            requestContext.Logger.Info("Using system browser.");
            return new DefaultOsBrowserWebUi(
                requestContext.ServiceBundle.PlatformProxy,
                requestContext.Logger,
                coreUIParent.SystemWebViewOptions);
        }
    }
}
