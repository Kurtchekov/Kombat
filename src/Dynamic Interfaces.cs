using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace kombat {
    public interface IDynamicAttackPower {

        float GetDynamicAttackPower(EntityAgent byEntity, ItemStack stack, Vec3d hitPosition, EntityAgent target);
    }

    public interface IDynamicAttackRange {

        float GetDynamicAttackRange(EntityPlayer player, ItemStack stack);
    }

    public interface IDynamicAttackKnockback {

        float GetDynamicAttackKnockback(ItemStack stack, DamageSource source, EntityAgent target);
    }
}
