using HarmonyLib;
using KitchenMods;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace KitchenRelaxedApplianceInfo {

    public class Mod : IModInitializer {

        public const string MOD_GUID = "blargle.relaxedapplianceinfo";
        public const string MOD_NAME = "Relaxed Appliance Info";
        public const string MOD_AUTHOR = "blargle";
        public static readonly string MOD_VERSION = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.ToString();

        public void PostActivate(KitchenMods.Mod mod) {
            Log($"{MOD_GUID} v{MOD_VERSION} in use!");
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MOD_GUID);
        }

        public void PreInject() {
            RelaxedApplianceInfoPreferences.registerPreferences();
        }

        public void PostInject() {
        }
            
        [Conditional("DEBUG")]
        public static void DebugLog(object message, [CallerFilePath] string callingFilePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null) {
            Log(message, callingFilePath, lineNumber, caller);
        }

        public static void Log(object message, [CallerFilePath] string callingFilePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null) {
            UnityEngine.Debug.Log($"[{MOD_GUID}] [{caller}({callingFilePath}:{lineNumber})] {message}");
        }
    }
}

