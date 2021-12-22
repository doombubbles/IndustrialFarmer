using System.Linq;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Extensions;
using UnhollowerBaseLib;
using UnityEngine;

namespace IndustrialFarmer.Displays
{
    public class IndustrialFarmerDisplay5 : IndustrialFarmerDisplay4
    {
        public override bool UseForTower(int[] tiers)
        {
            return tiers[0] == 20;
        }

        protected override void PostAddPitchfork(UnityDisplayNode node, GameObject pitchfork)
        {
            GameObject newPitchFork;
            SkinnedMeshRenderer pitchforkRenderer;
            string[] boneNames;
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

            node.genericRendererLayers = new Il2CppStructArray<int>(4);
        }
    }
}