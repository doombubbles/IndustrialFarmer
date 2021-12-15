using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using UnhollowerBaseLib;
using UnityEngine;

namespace IndustrialFarmer
{
    public class IndustrialFarmerMultiDisplay : ModTowerDisplay<IndustrialFarmer>
    {
        public override float Scale => 1.05f;

        private readonly int tier;

        public IndustrialFarmerMultiDisplay()
        {
        }

        public IndustrialFarmerMultiDisplay(int t)
        {
            tier = t;
        }

        public override string Name => GetType().Name + tier;

        public override string BaseDisplay =>
            GetDisplay(TowerType.EngineerMonkey, tier > 2 ? 3 : 0, tier <= 2 ? tier : 0);

        /// <summary>
        /// tiers[0] is the Hero's level
        /// </summary>
        /// <param name="tiers"></param>
        /// <returns></returns>
        public override bool UseForTower(int[] tiers)
        {
            switch (tier)
            {
                case 0:
                    return tiers[0] < 3;
                case 1:
                    return 3 <= tiers[0] && tiers[0] < 7;
                case 2:
                    return 7 <= tiers[0] && tiers[0] < 10;
                case 3:
                    return 10 <= tiers[0] && tiers[0] < 20;
                case 4:
                    return tiers[0] == 20;
                default:
                    return false;
            }
        }

        public override IEnumerable<ModContent> Load()
        {
            for (var i = 0; i <= 4; i++)
            {
                yield return new IndustrialFarmerMultiDisplay(i);
            }
        }

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            UseNode(GetDisplay(TowerType.BananaFarmer), bananaFarmer =>
            {
                var pitchfork = bananaFarmer.GetRenderers<SkinnedMeshRenderer>()[1].gameObject;

                var newPitchFork = Object.Instantiate(pitchfork, node.transform.GetChild(0));
                var pitchforkRenderer = newPitchFork.GetComponent<SkinnedMeshRenderer>();
                pitchforkRenderer.rootBone = node.GetBone("MonkeyRig:MonkeyJnt_Spine02");
                var boneNames = new[]
                {
                    "MonkeyRig:MonkeyJnt_Hand_R", "MonkeyRig:MonkeyJnt_Hand_R",
                    "MonkeyRig:MonkeyJnt_Hand_R", "MonkeyRig:MonkeyJnt_Hand_R",
                    "MonkeyRig:MonkeyJnt_Hand_R"
                };
                pitchforkRenderer.bones = boneNames.Select(node.GetBone).ToIl2CppReferenceArray();
                node.genericRenderers = node.genericRenderers.AddTo(pitchforkRenderer);

                if (tier == 4)
                {
                    newPitchFork = Object.Instantiate(pitchfork, node.transform.GetChild(0));
                    pitchforkRenderer = newPitchFork.GetComponent<SkinnedMeshRenderer>();
                    pitchforkRenderer.rootBone = node.GetBone("MonkeyRig:MonkeyJnt_Spine02");
                    boneNames = new[]
                    {
                        "MonkeyRig:Propjectile_L", "MonkeyRig:Propjectile_L",
                        "MonkeyRig:Propjectile_L", "MonkeyRig:Propjectile_L",
                        "MonkeyRig:Propjectile_L"
                    };
                    pitchforkRenderer.bones = boneNames.Select(node.GetBone).ToIl2CppReferenceArray();
                    node.genericRenderers = node.genericRenderers.AddTo(pitchforkRenderer);
                }
                
                node.genericRendererLayers = new Il2CppStructArray<int>(tier == 4 ? 5 : 4);
                node.RecalculateGenericRenderers();
            });

            node.RemoveBone("MonkeyRig:Propjectile_R");
            if (tier > 2)
            {
                node.RemoveBone("MonkeyRig:Propjectile_L");
            }
        }
    }
}