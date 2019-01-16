using Harmony;
using spacechase0.MiniModLoader.Api;
using spacechase0.MiniModLoader.Api.Mods;
using System.Reflection;

namespace AlwaysMinimapPortraits
{
    public class Mod : IMod
    {
        public override void AfterModsLoaded()
        {
            var harmony = HarmonyInstance.Create("spacechase0.Portia.AlwaysMinimapPortraits");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
