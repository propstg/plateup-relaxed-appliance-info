using HarmonyLib;
using Kitchen;
using System;
using TMPro;
using UnityEngine;

namespace KitchenRelaxedApplianceInfo {

    [HarmonyPatch(typeof(ApplianceInfoView), "UpdateData")]
    class ApplianceInfoView_Patch{

        public static bool Prefix(Animator ___Animator, GameObject ___Backing) {
            if (RelaxedApplianceInfoPreferences.getDisplayMode() == 1) {
                 ___Animator.enabled = false;
            }

            if (RelaxedApplianceInfoPreferences.isNoAnimation()) {
                ___Animator.speed = 1000f;
            }

            if (RelaxedApplianceInfoPreferences.isNoBackground()) {
                ___Backing.SetActive(false);
            }

            return true;
        }

        public static void Postfix(TextMeshPro ___Title, TextMeshPro ___Description, GameObject ___Sections, GameObject ___PriceTag, GameObject ___Backing, float ___TopTextHeight) {
            try {
                switch (RelaxedApplianceInfoPreferences.getDisplayMode()) {
                    case 2:
                        hideTitle(___Title);
                        hideDescription(___Description);
                        hideGameObject(___Sections);
                        setPriceTagPosition(___PriceTag, 3.5f);
                        setBackingSize(___Backing, ___TopTextHeight - 0.5f);
                        break;
                    case 3:
                        hideDescription(___Description);
                        hideGameObject(___Sections);
                        setPriceTagPosition(___PriceTag, 3f);
                        setBackingSize(___Backing, ___TopTextHeight);
                        break;
                }
            } catch (Exception e) {
                Mod.Log("Encountered exception when changing settings in postfix");
                Mod.Log(e);
            }
        }

        static void hideDescription(TextMeshPro descriptionTmp) {
            hideTextMeshPro(descriptionTmp);
        }

        static void hideTitle(TextMeshPro titleTmp) {
            hideTextMeshPro(titleTmp);
        }

        static void hideTextMeshPro(TextMeshPro tmp) {
            var gameObject = tmp?.transform?.gameObject;

            hideGameObject(gameObject);
        }

        static void hideGameObject(GameObject gameObject) {
            if (gameObject != default) {
                gameObject.SetActive(false);
            }
        }

        static void setPriceTagPosition(GameObject ___PriceTag, float y) {
            if (___PriceTag?.transform != null) {
            ___PriceTag.transform.localPosition = new Vector3(0.8f, y, 0f);
            }
        }

        static void setBackingSize(GameObject ___Backing, float z) {
            ___Backing.transform.localScale = ___Backing.transform.localScale with { z = z };
        }
    }
}

