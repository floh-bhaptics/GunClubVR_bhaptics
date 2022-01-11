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

        [HarmonyPatch(typeof(TBMVRDevice), "FireHaptics", new Type[] { typeof(Side), typeof(VibrationForce) })]
        public class bhaptics_WeaponHandlerFire3
        {
            [HarmonyPostfix]
            public static void Postfix(Side handSide, VibrationForce force)
            {
                if ((force == VibrationForce.Light) | (force == VibrationForce.None)) return;
                float intensity = 1.0f;
                if (force == VibrationForce.Medium) intensity = 0.7f;
                tactsuitVr.Recoil("Pistol", (handSide == Side.Right), intensity);
                //tactsuitVr.LOG("Haptics: " + handSide.ToString() + " " + force.ToString());
            }
        }


    }
}
