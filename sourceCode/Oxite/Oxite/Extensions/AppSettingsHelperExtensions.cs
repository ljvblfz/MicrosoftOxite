using Oxite.Infrastructure;

namespace Oxite.Extensions
{
    public static class AppSettingsHelperExtensions
    {
        public static string GetInstanceName(this AppSettingsHelper appSettingsHelper)
        {
            return appSettingsHelper.GetString("Oxite.InstanceName", "Oxite");
        }
    }
}