using System.Runtime.CompilerServices;
using Assets.Scripts.Models.Bloons.Behaviors;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Filters;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.TowerFilters;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Towers;
using IndustrialFarmer.Patches;
using UnhollowerBaseLib;

namespace IndustrialFarmer;

public class Levels
{
    public class Level2 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Collected Bananas are worth 10% more.";

        public override int Level => 2;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<CollectCashZoneModel>().name = "CollectCashZoneModel_" + Id;
        }
    }

    public class Level3 : ModHeroLevel<IndustrialFarmer>
    {
        public override string AbilityName => "Bloon Pesticide";

        public override string AbilityDescription =>
            "Spray Bloons in a large area with pesticide, damaging them over time.";

        public override string Description => $"{AbilityName}: {AbilityDescription}";

        public override int Level => 3;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var quincy = Game.instance.model.GetTowerWithName(TowerType.Quincy + " 10");
            var glue = Game.instance.model.GetTowerWithName(TowerType.GlueGunner + "-300");
            var glueStorm = Game.instance.model.GetTowerWithName(TowerType.GlueGunner + "-040");

            var dot = glue.GetDescendant<AddBehaviorToBloonModel>().Duplicate();
            dot.mutationId = "Pesticide";
            // dot.overlayLayer = 2; TODO
            // dot.overlays = glue.GetDescendant<SlowModel>().overlays;
            dot.glueLevel = 0;
            dot.collideThisFrame = true;

            var abilityModel = quincy.GetAbility(1).Duplicate();
            abilityModel.name = "AbilityModel_BloonPesticide";
            abilityModel.displayName = AbilityName;
            abilityModel.addedViaUpgrade = Id;
            abilityModel.icon = GetSpriteReference("BloonPesticide");
            abilityModel.RemoveBehavior<CreateSoundOnAbilityModel>();
            abilityModel.AddBehavior(glueStorm.GetDescendant<CreateSoundOnAbilityModel>());
            abilityModel.Cooldown = 30;
            towerModel.AddBehavior(abilityModel);

            var attack = abilityModel.GetBehavior<ActivateAttackModel>().attacks[0];
            attack.targetProvider = attack.GetBehavior<TargetStrongModel>();
            attack.RemoveBehavior<TargetFirstModel>();
            attack.RemoveBehavior<TargetCloseModel>();
            attack.RemoveBehavior<TargetLastModel>();

            var effectModel = attack.GetDescendant<CreateEffectOnExhaustFractionModel>().effectModel;
            effectModel.assetId = GetDisplayGUID<PesticideSplatter>();

            var projectile = attack.GetDescendant<CreateProjectileOnExhaustFractionModel>().projectile;
            projectile.RemoveBehavior<DamageModel>();
            projectile.RemoveBehavior<DamageModifierForTagModel>();
            projectile.GetBehavior<AgeModel>().Lifespan = .1f;
            projectile.RemoveBehavior<ClearHitBloonsModel>();
            projectile.GetBehavior<ProjectileFilterModel>().filters = projectile
                .GetBehavior<ProjectileFilterModel>().filters
                .RemoveItemOfType<FilterModel, FilterWithChanceModel>();
            projectile.collisionPasses = new[] {-1, 0};
            projectile.filters = projectile.GetBehavior<ProjectileFilterModel>().filters;
            projectile.radius = 50;
            projectile.AddBehavior(dot);
        }
    }

    public class Level4 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "All Banana Farms in radius get tier 1 upgrades for free.";

        public override int Level => 4;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.AddBehavior(new FreeUpgradeSupportModel("FreeUpgradesSupportModel_", 1,
                "IndustrialFarmer:FreeUpgrades",
                new Il2CppReferenceArray<TowerFilterModel>(new TowerFilterModel[]
                {
                    new FilterInBaseTowerIdModel("FilterInBaseTowerIdModel_",
                        new Il2CppStringArray(new[] {TowerType.BananaFarm}))
                })));
        }
    }


    public class Level5 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Banana Farms can be placed on nearby water.";

        public override int Level => 5;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.AddBehavior(new FreezeNearbyWaterModel("FreezeNearbyWaterModel_", towerModel.range, 1, ""));
        }
    }

    public class Level6 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "All Banana Farms in radius get tier 2 upgrades for free.";

        public override int Level => 6;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<FreeUpgradeSupportModel>().upgrade = 2;
        }
    }

    public class Level7 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Range is increased.";

        public override int Level => 7;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 15;
            towerModel.GetBehavior<CollectCashZoneModel>().attractRange += 15;
        }
    }

    public class Level8 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Banana Farms in radius and their upgrades cost 10% less.";

        public override int Level => 8;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.AddBehavior(new DiscountZoneModel("DiscountZoneModel_", .1f, 1,
                Discounts.IndustrialFarmerDiscount,
                "IndustrialFarmer", false, 0, "IndustrialFarmer", GetTextureGUID("IndustrialFarmer-Icon")));
        }
    }

    public class Level9 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Collected Bananas are now worth 15% more.";

        public override int Level => 9;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<CollectCashZoneModel>().name = "CollectCashZoneModel_" + Id;
        }
    }

    public class Level10 : ModHeroLevel<IndustrialFarmer>
    {
        public override string AbilityName => "Green Revolution";

        public override string AbilityDescription =>
            "Transforms some of the Largest Bloons on screen into Green Bloons. Targets up to BFBs.";

        public override string Description => $"{AbilityName}: {AbilityDescription}";

        public override int Level => 10;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var bma = Game.instance.model.GetTower(TowerType.Alchemist, 0, 0, 5);
            var techTerror = Game.instance.model.GetTower(TowerType.SuperMonkey, 0, 4);

            var shrink = bma.GetAttackModel(2).Duplicate();

            var filterModels = shrink.GetBehavior<AttackFilterModel>().filters.ToList();
            filterModels.Add(new FilterOutTagModel("FilterOutTagModel_1", "Red", new Il2CppStringArray(0)));
            filterModels.Add(new FilterOutTagModel("FilterOutTagModel_2", "Blue", new Il2CppStringArray(0)));
            filterModels.Add(new FilterOutTagModel("FilterOutTagModel_3", "Green", new Il2CppStringArray(0)));
            filterModels.Add(new FilterOutTagModel("FilterOutTagModel_4", "Ddt", new Il2CppStringArray(0)));
            filterModels.Add(new FilterOutTagModel("FilterOutTagModel_5", "Zomg", new Il2CppStringArray(0)));

            var projectile = shrink.GetDescendant<CreateProjectileOnExhaustFractionModel>().projectile;
            projectile.radius = 50;
            projectile.GetBehavior<MorphBloonModel>().bloonId = "Green";
            projectile.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);

            var abilityModel = towerModel.GetAbility().Duplicate();
            abilityModel.name = "AbilityModel_GreenRevolution";
            abilityModel.displayName = AbilityName;
            abilityModel.addedViaUpgrade = Id;
            abilityModel.GetDescendant<AttackFilterModel>().filters = filterModels.ToIl2CppReferenceArray();
            abilityModel.GetDescendant<CreateProjectileOnExhaustFractionModel>().projectile = projectile;
            abilityModel.GetDescendant<CreateEffectOnExhaustFractionModel>().effectModel =
                techTerror.GetDescendant<CreateEffectOnAbilityModel>().effectModel;
            abilityModel.GetDescendant<CreateProjectileOnExhaustFractionModel>().AddChildDependant(projectile);
            abilityModel.RemoveBehavior<CreateSoundOnAbilityModel>();
            abilityModel.Cooldown = 60;
            abilityModel.icon = GetSpriteReference("GreenRevolution");
            towerModel.AddBehavior(abilityModel);
        }
    }

    public class Level11 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Bloon Pesticide's damage over time is significantly increased";

        public override int Level => 11;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var dot = towerModel.GetAbility(0).GetDescendant<DamageOverTimeModel>();

            dot.Interval = .1f;
        }
    }

    public class Level12 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Further increased range";

        public override int Level => 12;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 15;
            towerModel.GetBehavior<CollectCashZoneModel>().attractRange += 15;
        }
    }

    public class Level13 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Collected Bananas Crates are now worth 20% more.";

        public override int Level => 13;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<CollectCashZoneModel>().name = "CollectCashZoneModel_" + Id;
        }
    }

    public class Level14 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Bloon Pesticide makes Bloons take additional damage from attacks.";

        public override int Level => 14;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var superBrittle = Game.instance.model.GetTower(TowerType.IceMonkey, 5);

            var brittler = superBrittle.GetDescendant<AddBonusDamagePerHitToBloonModel>().Duplicate();
            var projectile = towerModel.GetAbility(0).GetDescendant<CreateProjectileOnExhaustFractionModel>()
                .projectile;
            var addBehavior = projectile.GetBehavior<AddBehaviorToBloonModel>();

            brittler.mutationId = "PesticideDamageBonus";
            brittler.layers = addBehavior.layers;
            brittler.lifespan = addBehavior.lifespan;
            brittler.lifespanFrames = addBehavior.lifespanFrames;

            projectile.AddBehavior(brittler);
        }
    }


    public class Level15 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Banana Farms in radius and their upgrades now cost 20% less.";

        public override int Level => 15;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<DiscountZoneModel>().discountMultiplier = .2f;
        }
    }


    public class Level16 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Green Revolution can hit more Bloons, including ZOMGs and DDTs.";

        public override int Level => 16;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectile = towerModel.GetAbility(1).GetDescendant<CreateProjectileOnExhaustFractionModel>()
                .projectile;
            projectile.pierce += 100;

            var attackFilterModel = towerModel.GetAbility(1).GetDescendant<AttackFilterModel>();
            var filterModels = attackFilterModel.filters.ToList();
            filterModels.RemoveAll(model => model.IsType<FilterOutTagModel>(out var tag) && tag.tag == "Ddt");
            filterModels.RemoveAll(model => model.IsType<FilterOutTagModel>(out var tag) && tag.tag == "Zomg");
            attackFilterModel.filters = filterModels.ToIl2CppReferenceArray();
        }
    }

    public class Level17 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Bloon Pesticide's damage over time is massively increased.";

        public override int Level => 17;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var dot = towerModel.GetAbility(0).GetDescendant<DamageOverTimeModel>();

            dot.damage = 5f;
        }
    }


    public class Level18 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Further increased range";

        public override int Level => 18;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 15;
            towerModel.GetBehavior<CollectCashZoneModel>().attractRange += 15;
        }
    }


    public class Level19 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Collected Bananas Crates are now worth 25% more.";

        public override int Level => 19;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<CollectCashZoneModel>().name = "CollectCashZoneModel_" + Id;
        }
    }

    public class Level20 : ModHeroLevel<IndustrialFarmer>
    {
        public override string Description => "Green Revolution affects even more Bloons and has reduced cooldown.";

        public override int Level => 20;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var abilityModel = towerModel.GetAbility(1);
            abilityModel.Cooldown = 45;

            var projectile = abilityModel.GetDescendant<CreateProjectileOnExhaustFractionModel>().projectile;
            projectile.pierce += 100;
        }
    }
}