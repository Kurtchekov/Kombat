using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace kombat {
    static public class PlayerControlUtils {

        public static bool IsEntityControlledByClient(EntityAgent toCheck) {
            return
                toCheck.Api.Side == EnumAppSide.Server ? false :
                ((ICoreClientAPI)toCheck.Api).IsSinglePlayer ? true :
                ((ICoreClientAPI)toCheck.Api).World.Player.Entity == toCheck;
        }

        public static bool[] disabledActions = new bool[13];

        public static void DisableAll() {
            DisableMovement();
            DisableModifiers();
            DisableToggles();
            DisableInteractions();
        }

        public static void EnableAll() {
            EnableMovement();
            EnableModifiers();
            EnableToggles();
            EnableInteractions();
        }

        public static void DisableInteractions() {
            DisableLeftMouse();
            DisableRightMouse();
        }

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

        public static void DisableMovement() {
            disabledActions[0] = true;
            disabledActions[1] = true;
            disabledActions[2] = true;
            disabledActions[3] = true;
            disabledActions[11] = true;
            disabledActions[12] = true;
        }

        public static void EnableMovement() {
            disabledActions[0] = false;
            disabledActions[1] = false;
            disabledActions[2] = false;
            disabledActions[3] = false;
            disabledActions[11] = false;
            disabledActions[12] = false;
        }

        public static void DisableModifiers() {
            disabledActions[5] = true;
            disabledActions[6] = true;
        }

        public static void EnableModifiers() {
            disabledActions[5] = false;
            disabledActions[6] = false;
        }

        public static void DisableToggles() {
            disabledActions[4] = true;
            disabledActions[7] = true;
            disabledActions[8] = true;
        }

        public static void EnableToggles() {
            disabledActions[4] = false;
            disabledActions[7] = false;
            disabledActions[8] = false;
        }
    }
}
