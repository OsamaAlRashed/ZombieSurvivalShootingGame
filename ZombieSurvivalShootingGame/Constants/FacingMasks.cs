using System.Collections;

namespace ZombieSurvivalShootingGame.Constants
{
    public static class FacingMasks
    {
        private static readonly BitArray upMask = new BitArray(new int[] { 1, 0, 0, 0 });
        private static readonly BitArray rightMask = new BitArray(new int[] { 0, 1, 0, 0 });
        private static readonly BitArray downMask = new BitArray(new int[] { 0, 0, 1, 0 });
        private static readonly BitArray leftMask = new BitArray(new int[] { 0, 0, 0, 1 });

        public static void GoLeft(this BitArray currentState) 
            => currentState.Or(leftMask);

        public static void StopLeft(this BitArray currentState)
            => currentState = currentState.And(leftMask.Not());

        public static void GoRight(this BitArray currentState)
            => currentState.Or(rightMask);

        public static void StopRight(this BitArray currentState)
            => currentState = currentState.And(rightMask.Not());

        public static void GoUp(this BitArray currentState)
            => currentState.Or(upMask);

        public static void StopUp(this BitArray currentState)
            => currentState = currentState.And(upMask.Not());

        public static void GoDown(this BitArray currentState)
            => currentState.Or(downMask);

        public static void StopDown(this BitArray currentState)
            => currentState = currentState.And(downMask.Not());
    }
}
