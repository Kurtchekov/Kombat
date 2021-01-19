using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace kombat {
    public interface IDynamicAttackPower {

        /// <summary>
        /// Implement this if you need to have damage depend on more nuanced conditions. Use stack.Collectible.Attributes["attr_name"] to retrieve definitions from Json files!
        /// </summary>
        /// <param name="byEntity">A reference to the attacking entity.</param>
        /// <param name="stack">A reference to the item that was used to attack.</param>
        /// <param name="hitPosition">A reference to the specific coordinate where the attack overlaps with the attacked entity's collision box.</param>
        /// <param name="target">A reference to the attacked entity.</param>
        /// <returns>how much damage should this attack deal.</returns>
        float GetDynamicAttackPower(EntityAgent byEntity, ItemStack stack, Vec3d hitPosition, EntityAgent target);
    }

    public interface IDynamicAttackRange {

        /// <summary>
        /// Implement this if you need to have range depend on more nuanced conditions. 
        /// Doesn't have a target reference because this value is used to determine where the attack is going to take place.
        /// Use stack.Collectible.Attributes["attr_name"] to retrieve definitions from Json files!
        /// </summary>
        /// <param name="player">A reference to the attacking player.</param>
        /// <param name="stack">A reference to the item that is being used to attack.</param>
        /// <returns></returns>
        float GetDynamicAttackRange(EntityPlayer player, ItemStack stack);
    }

    public interface IDynamicAttackKnockback {

        /// <summary>
        /// Implement this if you need to have specific knockback. Use stack.Collectible.Attributes["attr_name"] to retrieve definitions from Json files!
        /// </summary>
        /// <param name="stack">A reference to the item that is being used to attack.</param>
        /// <param name="source">A reference to the DamageSource object created by the attack. Contains information about the attacker!</param>
        /// <param name="target">A reference to the attacked entity.</param>
        /// <returns></returns>
        float GetDynamicAttackKnockback(ItemStack stack, DamageSource source, EntityAgent target);
    }
}
