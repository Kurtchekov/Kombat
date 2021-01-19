using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace kombat {
    class Kombat : ModSystem {

        Harmony patches;

        public override void Dispose() {
            base.Dispose();
            patches.UnpatchAll();
        }

        public override void Start(ICoreAPI api) {
            base.Start(api);
            patches = new Harmony("Kombat.Harmony");
            patches.PatchAll();
        }

        public override void StartClientSide(ICoreClientAPI api) {
            base.StartClientSide(api);
            api.Input.InWorldAction += Input_InWorldAction;
        }

        private static void Input_InWorldAction(EnumEntityAction action, bool on, ref EnumHandling handled) {
            if(PlayerControlUtils.disabledActions[(int)action])
                handled = EnumHandling.PreventDefault;
        }

        private static float GetDynamicAttackRange(ItemStack stack, EntityPlayer player) {
            return stack.Item is IDynamicAttackRange ?
                ((IDynamicAttackRange)stack.Item).GetDynamicAttackRange(player, stack) :
                stack.Collectible.GetAttackRange(stack);
        }

        private static float GetDynamicAttackPower(EntityAgent byEntity, ItemSlot slot, Vec3d hitPosition, EntityAgent target) {
            return slot.Itemstack.Item is IDynamicAttackPower ?
                ((IDynamicAttackPower)slot.Itemstack.Item).GetDynamicAttackPower(byEntity, slot.Itemstack, hitPosition, target) :
                slot.Itemstack.Collectible.GetAttackPower(slot.Itemstack);
        }

        private static void DidDamageExtended(ItemSlot slot, DamageSource source, EntityAgent target) {
            if(target.World.Side == EnumAppSide.Client)
                return;
            Vec3d vec3d = (target.SidedPos.XYZ - (source.SourceEntity == null ? source.SourcePos : source.SourceEntity.SidedPos.XYZ)).Normalize();
            vec3d.Y = 0.1;
            float force = (slot?.Itemstack?.Item is IDynamicAttackKnockback) ?
                ((IDynamicAttackKnockback)slot.Itemstack.Item).GetDynamicAttackKnockback(slot.Itemstack, source, target) : 1f;
            float single = GameMath.Clamp((force - target.Properties.KnockbackResistance) / 10f, 0f, 2f);
            target.SidedPos.Motion.Add(vec3d.X * (double)single, vec3d.Y * (double)single, vec3d.Z * (double)single);
            target.DidAttack(source, (EntityAgent)source.SourceEntity);
        }

    }
}
