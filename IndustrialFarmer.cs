﻿using System.Collections.Generic;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity;
using BTD_Mod_Helper.Api.Towers;

namespace IndustrialFarmer;

public class IndustrialFarmer : ModHero
{
    public override string BaseTower => TowerType.BananaFarmer;

    public override int Cost => 1400;

    public override string DisplayName => "Norman";
    public override string Title => "Industrial Farmer";
    public override string Level1Description => "Collects nearby bananas. Your next Banana Farm is free.";

    public override string Description =>
        "Norman collects your Bananas and helps you expand your farming operations.";


    public override string NameStyle => TowerType.Gwendolin; // Yellow colored
    public override string BackgroundStyle => TowerType.Etienne; // Yellow colored
    public override string GlowStyle => TowerType.StrikerJones; // Yellow colored

    public override float XpRatio => 1.0f;

    public override string RogueStarterArtifact => "TheUnspokenHeroes1";
    public override IEnumerable<(string, int[])> RogueStarterInstas =>
    [
        (TowerType.BananaFarm, [0, 0, 0]),
        (TowerType.EngineerMonkey, [0, 0, 2])
    ];

    /// <param name="towerModel"></param>
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var quincy = Game.instance.model.GetTowerWithName(TowerType.Quincy);
        towerModel.AddBehavior(quincy.GetBehavior<CreateSoundOnUpgradeModel>().Duplicate());
        towerModel.AddBehavior(quincy.GetBehavior<CreateEffectOnUpgradeModel>().Duplicate());

        towerModel.radius = quincy.radius;
        towerModel.footprint = quincy.footprint.Duplicate();
    }

    public const string IndustrialFarmerDiscount = "IndustrialFarmerDiscount";
}