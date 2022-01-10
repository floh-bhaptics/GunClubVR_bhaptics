using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MelonLoader;
using HarmonyLib;
using MyBhapticsTactsuit;

namespace GunClubVR_bhaptics
{
    public class GunClubVR_bhaptics : MelonMod
    {
        public static TactsuitVR tactsuitVr;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            tactsuitVr = new TactsuitVR();
            tactsuitVr.PlaybackHaptics("HeartBeat");
        }

        [HarmonyPatch(typeof(WeaponInteraction), "FireHaptics", new Type[] { typeof(int) })]
        public class bhaptics_WeaponHandlerFire
        {
            [HarmonyPostfix]
            public static void Postfix(WeaponInteraction __instance, int side)
            {
                tactsuitVr.LOG("Fire side: " + side.ToString());
                tactsuitVr.Recoil("Pistol", true);
            }
        }

    }
}
