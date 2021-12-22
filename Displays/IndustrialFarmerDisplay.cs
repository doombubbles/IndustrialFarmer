using System.Linq;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using UnhollowerBaseLib;
using UnityEngine;

namespace IndustrialFarmer.Displays
{
    public abstract class IndustrialFarmerDisplay : ModTowerDisplay<IndustrialFarmer>
    {
        public override bool ModifiesUnityObject => true;

        public override float Scale => 1.05f;

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

                PostAddPitchfork(node, pitchfork);
                
                node.RecalculateGenericRenderers();
            });

            node.RemoveBone("MonkeyRig:Propjectile_R");
        }


        protected  virtual void PostAddPitchfork(UnityDisplayNode node, GameObject pitchfork)
        {
            node.genericRendererLayers = new Il2CppStructArray<int>(4);
        }
    }
}