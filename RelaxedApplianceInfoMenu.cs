using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace KitchenRelaxedApplianceInfo {

    public class RelaxedApplianceInfoMenu : Menu<MenuAction> {

        public RelaxedApplianceInfoMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int player_id) {
            AddLabel("Display mode");
            Add(new Option<int>(new List<int> { 0, 1, 2, 3 }, RelaxedApplianceInfoPreferences.getDisplayMode(), new List<string> { "Full Info", "Never display, even when pinged", "Coins only", "Name and coins"}))
                .OnChanged += delegate (object _, int value) {
                    RelaxedApplianceInfoPreferences.setDisplayMode(value);
                };
            AddLabel("Display instantly instead of zooming/bouncing into view");
            Add(new Option<bool>(new List<bool> { false, true }, RelaxedApplianceInfoPreferences.isNoAnimation(), new List<string> { "No", "Yes" }))
                .OnChanged += delegate (object _, bool value) {
                    RelaxedApplianceInfoPreferences.setNoAnimation(value);
                };
            AddLabel("Remove background from popup");
            Add(new Option<bool>(new List<bool> { false, true }, RelaxedApplianceInfoPreferences.isNoBackground(), new List<string> { "No", "Yes" }))
                .OnChanged += delegate (object _, bool value) {
                    RelaxedApplianceInfoPreferences.setNoBackground(value);
                };

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }
    }

    [HarmonyPatch(typeof(AccessibilityMenu<MenuAction>), "Setup")]
    class AccessibilityMenu_Patch {

        public static bool Prefix(MainMenu __instance) {
            MethodInfo addSubmenu = __instance.GetType().GetMethod("AddSubmenuButton", BindingFlags.NonPublic | BindingFlags.Instance);
            addSubmenu.Invoke(__instance, new object[] { "Appliance Info", typeof(RelaxedApplianceInfoMenu), false });
            return true;
        }
    }

    [HarmonyPatch(typeof(PlayerPauseView), "SetupMenus")]
    class PauseMenu_Patch {

        public static bool Prefix(PlayerPauseView __instance) {
            ModuleList moduleList = (ModuleList)__instance.GetType().GetField("ModuleList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            MethodInfo addMenu = __instance.GetType().GetMethod("AddMenu", BindingFlags.NonPublic | BindingFlags.Instance);
            addMenu.Invoke(__instance, new object[] { typeof(RelaxedApplianceInfoMenu), new RelaxedApplianceInfoMenu(__instance.ButtonContainer, moduleList) });
            return true;
        }
    }
}

