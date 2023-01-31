using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Utils;
using BTD_Mod_Helper.Api.Display;

namespace IndustrialFarmer;

public class PesticideSplatter : ModDisplay
{
    public override PrefabReference BaseDisplayReference => Game.instance.model.GetTower(TowerType.GlueGunner, 3, 2)
        .GetDescendant<CreateEffectOnContactModel>().effectModel.assetId;

    public override float Scale => 3f;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
    }
}