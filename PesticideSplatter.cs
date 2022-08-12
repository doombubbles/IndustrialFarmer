using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Display;
using Assets.Scripts.Utils;
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