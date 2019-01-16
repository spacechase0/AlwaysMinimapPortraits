using Harmony;
using Pathea.ActorNs;
using Pathea.MapNs;
using Pathea.ModuleNs;
using Pathea.UISystemNs;
using System.Reflection;
using UnityEngine;

namespace AlwaysMinimapPortraits
{
    [HarmonyPatch(typeof(Actor_IMap))]
    [HarmonyPatch("Pathea.MapNs.IMap.GetIconInfo")]
    public static class GetIconInfoPatch
    {
        private static FieldInfo overrideSpriteField = null;
        
        public static void Postfix(Actor_IMap __instance, ref PresetIcon ___iconInfo, ref Actor ___mActor)
        {
            if ( overrideSpriteField is null )
            {
                overrideSpriteField = typeof(PresetIcon).GetField("overrideSprite", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            }

            var sprite = overrideSpriteField.GetValue(___iconInfo);

            if ( __instance.GetIcon() == MapIcon.NPC )
            {
                ActorInfo actorInfo = Module<ActorMgr>.Self.GetActorInfo(___mActor.TmpltId);
                if (!string.IsNullOrEmpty(actorInfo.miniHeadIcon))
                {
                    if (!actorInfo.miniHeadIcon.Contains("Dina")) // This doesn't exist for some reason
                        ___iconInfo.SetOverrideSprite(UIUtils.GetSpriteByPath(actorInfo.miniHeadIcon), false, Vector2.zero);
                }
                else
                {
                    if (!actorInfo.icon.Contains("Dina")) // This doesn't exist for some reason
                        ___iconInfo.SetOverrideSprite(UIUtils.GetSpriteByPath(actorInfo.icon), true, new Vector2(30f, 31.5f));
                }
            }
        }
    }
}
