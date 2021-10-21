using System.Runtime.InteropServices;

namespace NativeMessaging
{
    /// <summary>
    /// Provides methods to identify the operating system platform executing
    /// the application.
    /// </summary>
    public static class OperatingSystemPlatform
    {
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsMacOS()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }

        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
    }
}
