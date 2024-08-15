using ETicaret.Debugging;

namespace ETicaret
{
    public class ETicaretConsts
    {
        public const string LocalizationSourceName = "ETicaret";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "03f5b1b0bcf24d7c81868e634b3dcc57";
    }
}
