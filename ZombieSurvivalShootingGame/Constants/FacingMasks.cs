using System.Collections;
using System.Windows.Forms;

namespace ZombieSurvivalShootingGame.Constants
{
    public static class FacingMasks
    {
        public static bool Get(this BitArray currentState, Keys keys)
        {
            switch (keys)
            {
                case Keys.Left:
                    return currentState.GetLeft();
                case Keys.Right:
                    return currentState.GetRight();
                case Keys.Up:
                    return currentState.GetUp();
                case Keys.Down:
                    return currentState.GetDown();
            }

            ///TODO
            return false;
        }

        public static void Stop(this BitArray currentState, Keys keys)
        {
            switch (keys)
            {
                case Keys.Left:
                    currentState.StopLeft();
                    break;
                case Keys.Right:
                    currentState.StopRight();
                    break;
                case Keys.Up:
                    currentState.StopUp();
                    break;
                case Keys.Down:
                    currentState.StopDown();
                    break;
            }
        }

        public static void Go(this BitArray currentState) => currentState.Set(0, true);
        public static void Stop(this BitArray currentState) => currentState.Set(0, false);


        public static bool GetUp(this BitArray currentState) => currentState.Get(0);
        public static void GoUp(this BitArray currentState) => currentState.Set(0, true);
        public static void StopUp(this BitArray currentState) => currentState.Set(0, false);

        public static bool GetRight(this BitArray currentState) => currentState.Get(1);
        public static void GoRight(this BitArray currentState) => currentState.Set(1, true);
        public static void StopRight(this BitArray currentState) => currentState.Set(1, false);

        public static bool GetDown(this BitArray currentState) => currentState.Get(2);
        public static void GoDown(this BitArray currentState) => currentState.Set(2, true);
        public static void StopDown(this BitArray currentState) => currentState.Set(2, false);

        public static bool GetLeft(this BitArray currentState) => currentState.Get(3);
        public static void GoLeft(this BitArray currentState) => currentState.Set(3, true);
        public static void StopLeft(this BitArray currentState) => currentState.Set(3, false);
    }
}
