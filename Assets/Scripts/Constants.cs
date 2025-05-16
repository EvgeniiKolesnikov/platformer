using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static class AnimatorData
    {
        public static readonly int SpeedX = Animator.StringToHash(nameof(SpeedX));
        public static readonly int SpeedY = Animator.StringToHash(nameof(SpeedY));
        public static readonly string Idle = "Idle";
    }
    public static class InputData
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
    }
}
