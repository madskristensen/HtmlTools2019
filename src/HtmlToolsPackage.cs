using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace HtmlTools
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [Guid("d87af1f6-eee2-4a46-885a-372d215b98a3")]
    public sealed class HtmlToolsPackage : AsyncPackage
    {


    }
}
