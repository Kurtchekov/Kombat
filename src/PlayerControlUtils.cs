using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace kombat {
    static public class PlayerControlUtils {

        /// <summary>
        /// Use this to check if, when playing multiplayer, an EntityPlayer is representing the local player.
        /// Can be useful to check for this before using Disable/Enable actions.
        /// </summary>
        /// <param name="toCheck">A reference to the entity that will be checked.</param>
        /// <returns></returns>
        public static bool IsEntityControlledByClient(EntityAgent toCheck) {
            return
                toCheck.Api.Side == EnumAppSide.Server ? false :
                ((ICoreClientAPI)toCheck.Api).IsSinglePlayer ? true :
                ((ICoreClientAPI)toCheck.Api).World.Player.Entity == toCheck;
        }

        public static bool[] disabledActions = new bool[13];

        /// <summary>
        /// Player won't be able to use any actions. Does NOT cancel ongoing actions, use entity.Controls.StopAllMovement() for that.
        /// </summary>
        public static void DisableAll() {
            DisableMovement();
            DisableModifiers();
            DisableToggles();
            DisableInteractions();
        }

        /// <summary>
        /// Re-enables Player actions.
        /// </summary>
        public static void EnableAll() {
            EnableMovement();
            EnableModifiers();
            EnableToggles();
            EnableInteractions();
        }

        /// <summary>
        /// Disables mouse actions.
        /// </summary>
        public static void DisableInteractions() {
            DisableLeftMouse();
            DisableRightMouse();
        }

        /// <summary>
        /// Enables mouse actions.
        /// </summary>
        public static void EnableInteractions() {
            EnableLeftMouse();
            EnableRightMouse();
        }

        public static void DisableLeftMouse() {
            disabledActions[9] = true;
        }

        public static void EnableLeftMouse() {
            disabledActions[9] = false;
        }

        public static void DisableRightMouse() {
            disabledActions[10] = true;
        }

        public static void EnableRightMouse() {
            disabledActions[10] = false;
        }

        /// <summary>
        /// Disables left, right, forwards, backwards, up and down movements
        /// </summary>
        public static void DisableMovement() {
            disabledActions[0] = true;
            disabledActions[1] = true;
            disabledActions[2] = true;
            disabledActions[3] = true;
            disabledActions[11] = true;
            disabledActions[12] = true;
        }

        /// <summary>
        /// Enables left, right, forwards, backwards, up and down movements
        /// </summary>
        public static void EnableMovement() {
            disabledActions[0] = false;
            disabledActions[1] = false;
            disabledActions[2] = false;
            disabledActions[3] = false;
            disabledActions[11] = false;
            disabledActions[12] = false;
        }

        /// <summary>
        /// Disables sneak and sprint.
        /// </summary>
        public static void DisableModifiers() {
            disabledActions[5] = true;
            disabledActions[6] = true;
        }

        /// <summary>
        /// Enables sneak and sprint.
        /// </summary>
        public static void EnableModifiers() {
            disabledActions[5] = false;
            disabledActions[6] = false;
        }

        /// <summary>
        /// Disables jump and sit actions.
        /// </summary>
        public static void DisableToggles() {
            disabledActions[4] = true;
            disabledActions[7] = true;
            disabledActions[8] = true;
        }

        /// <summary>
        /// Enables jump and sit actions.
        /// </summary>
        public static void EnableToggles() {
            disabledActions[4] = false;
            disabledActions[7] = false;
            disabledActions[8] = false;
        }
    }
}
