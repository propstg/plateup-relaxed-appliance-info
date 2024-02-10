using Kitchen;

namespace KitchenRelaxedApplianceInfo {

    public class RelaxedApplianceInfoPreferences {

        public static readonly Pref DisplayMode = new Pref(Mod.MOD_GUID, nameof(DisplayMode));
        public static readonly Pref NoAnimation = new Pref(Mod.MOD_GUID, nameof(NoAnimation));
        public static readonly Pref NoBackground = new Pref(Mod.MOD_GUID, nameof(NoBackground));

        public static bool preferencesLoaded = false;

        public static void registerPreferences() {
            if (!preferencesLoaded) {
                preferencesLoaded = true;
                Preferences.AddPreference<int>(new IntPreference(DisplayMode, 0));
                Preferences.AddPreference<bool>(new BoolPreference(NoAnimation, true));
                Preferences.AddPreference<bool>(new BoolPreference(NoBackground, false));
                Preferences.Load();
            }
        }

        public static int getDisplayMode() {
            return Preferences.Get<int>(DisplayMode);
        }

        public static bool isNoAnimation() {
            return Preferences.Get<bool>(NoAnimation);
        }

        public static bool isNoBackground() {
            return Preferences.Get<bool>(NoBackground);
        }

        public static void setDisplayMode(int value) {
            Preferences.Set<int>(DisplayMode, value);
        }

        public static void setNoAnimation(bool value) {
            Preferences.Set<bool>(NoAnimation, value);
        }

        public static void setNoBackground(bool value) {
            Preferences.Set<bool>(NoBackground, value);
        }
    }
}

