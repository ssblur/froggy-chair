using TinyLife.Actions;
using TinyLife.Actions.Typeless;
using HarmonyLib;
using MLEM.Sound;
using Microsoft.Xna.Framework.Audio;
using MLEM.Data.Content;
using TinyLife.Objects;
using System.Collections.Generic;

namespace FroggySetMod {


    [HarmonyPatch]
    public class SitToot {
        static SoundEffect toot;
        static SoundEffect untoot;
        static Dictionary<Person, bool> Sitting = new();
        public static void Init(RawContentManager manager) {
            toot = manager.Load<SoundEffect>("toot");
            untoot = manager.Load<SoundEffect>("untoot");
        }

        [HarmonyPatch(typeof(TypelessAction), nameof(TypelessAction.Sit))] 
        static void Prefix(Person person, Furniture chair, float speedMultiplier, ActionSpot spot = null) {
            if(
                chair is FroggyChair &&
                !Sitting.GetValueOrDefault(person, false)
            ) {
                TinyLife.GameImpl.Instance.Map.PlaySound(toot, person.Position);
                Sitting.Add(person, true);
            }
        }

        [HarmonyPatch(typeof(MultiAction), nameof(MultiAction.OnCompleted))]    
        static void Postfix(CompletionType type, MultiAction __instance) {
            if(
                __instance is SitAction &&
                __instance.Info.GetInvolvedObject<FroggyChair>() is not null &&
                type == CompletionType.Completed
            ) {
                TinyLife.GameImpl.Instance.Map.PlaySound(untoot, __instance.Info.Person.Position);
                Sitting.Remove(__instance.Person);
            }
        }
    }
    
}