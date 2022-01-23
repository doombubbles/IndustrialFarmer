using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.Upgrade;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using NinjaKiwi.Common;
using UnityEngine.UI;

namespace AbilityChoice
{
    public abstract class ModAbilityChoice : ModVanillaUpgrade
    {
        public static readonly Dictionary<string, ModAbilityChoice> Cache = new Dictionary<string, ModAbilityChoice>();

        protected abstract string Description1 { get; }
        protected virtual string Description2 => "";

        protected virtual string AbilityName => Regex.Replace(
            Name,
            "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
            " $1",
            RegexOptions.Compiled).Trim();


        protected TowerModel BaseTowerModel { get; private set; }

        protected string DefaultDescription { get; set; }

        protected MelonPreferences_Entry<int> setting;

        private string RealDescription
        {
            get => LocalizationManager.Instance.textTable[UpgradeId + " Description"];
            set => LocalizationManager.Instance.textTable[UpgradeId + " Description"] = value;
        }

        public bool Enabled => setting.Value == 1 || setting.Value == 2;
        protected bool Mode2 => setting.Value == 2;

        public override void Register()
        {
            base.Register();
            BaseTowerModel = GetAffected(Game.instance.model).First();
            Cache[UpgradeId] = this;
            setting = Main.AbilityChoiceSettings.CreateEntry(Id, 1);
            DefaultDescription = RealDescription;
            UpdateDescription();

            //MelonLogger.Msg($"Registered AbilityChoice {Name} for upgrade {UpgradeId} and value {setting.Value}");
        }

        public bool updated;

        public void Toggle()
        {
            setting.Value++;
            if (setting.Value > 2 || Mode2 && string.IsNullOrEmpty(Description2))
            {
                setting.Value = 0;
            }

            UpdateDescription();
            updated = true;
        }

        private void UpdateDescription()
        {
            switch (setting.Value)
            {
                case 1:
                    RealDescription = Description1;
                    break;
                case 2:
                    RealDescription = Description2;
                    break;
                default:
                    RealDescription = DefaultDescription;
                    break;
            }
        }

        public abstract void Apply1(TowerModel model);

        public virtual void Apply2(TowerModel model)
        {
            
        }

        public sealed override void Apply(TowerModel towerModel)
        {
            if (Mode2)
            {
                Apply2(towerModel);
            }
            else
            {
                Apply1(towerModel);
            }
            
            RemoveAbility(towerModel);
        }

        public override IEnumerable<TowerModel> GetAffected(GameModel gameModel)
        {
            if (setting != null && !Enabled)
            {
                return Enumerable.Empty<TowerModel>();
            }

            return base.GetAffected(gameModel)
                .Where(model => AbilityModel(model) != null)
                .OrderBy(model => model.appliedUpgrades.Length);
        }

        public static void HandleIcon(UpgradeDetails upgradeDetails, bool update = false)
        {
            var abilityObject = upgradeDetails.abilityObject;
            var circle = abilityObject.GetComponent<Image>();
            if (upgradeDetails.AbilityChoice() is ModAbilityChoice abilityChoice)
            {
                if (update && !abilityChoice.updated)
                {
                    return;
                }

                abilityObject.SetActive(true);
                switch (abilityChoice.setting.Value)
                {
                    case 1:
                        circle.SetSprite(VanillaSprites.NotifyRed);
                        break;
                    case 2:
                        circle.SetSprite(VanillaSprites.NotifyBlue);
                        break;
                    default:
                        circle.SetSprite(VanillaSprites.NotificationYellow);
                        break;
                }

                abilityChoice.updated = false;
            }
            else
            {
                circle.SetSprite(VanillaSprites.NotificationYellow);
            }
        }

        public virtual AbilityModel AbilityModel(TowerModel model)
        {
            return model.GetBehaviors<AbilityModel>()
                .FirstOrDefault(abilityModel => abilityModel.displayName == AbilityName);
        }

        public virtual void RemoveAbility(TowerModel model)
        {
            var abilityModel = AbilityModel(model);
            if (abilityModel != null)
            {
                model.RemoveBehavior(abilityModel);
            }
            else
            {
                // MelonLogger.Warning($"Couldn't apply ModAbilityChoice {Name}");
            }
        }


        protected float CalcAvgBonus(float uptime, float dpsMult)
        {
            return uptime * dpsMult + (1 - uptime);
        }

        protected void TechBotify(TowerModel model)
        {
            var ability = AbilityModel(model);

            ability.enabled = false;
            var behavior =
                new ActivateAbilityAfterIntervalModel("ActivateAbilityAfterIntervalModel", ability,
                    ability.Cooldown);
            model.AddBehavior(behavior);
        }
    }
}