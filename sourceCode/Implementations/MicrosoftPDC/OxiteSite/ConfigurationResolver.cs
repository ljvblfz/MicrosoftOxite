namespace OxiteSite
{
    public class ConfigurationResolver
    {
        public static string GetEventName()
        {
            // Because of dynamic compilation issues, this file is necessary to "wake up"
            // the MIX10 conditional compilation symbol
#if MIX10
            return "mix10";
#else
            return "pdc09";
#endif
        }
    }
}
