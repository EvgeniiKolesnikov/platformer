using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static class AnimatorData
    {
        public static readonly int SpeedX = Animator.StringToHash(nameof(SpeedX));
        public static readonly int SpeedY = Animator.StringToHash(nameof(SpeedY));
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));

        public static readonly int IsOn = Animator.StringToHash(nameof(IsOn));
        public static readonly int IsOff = Animator.StringToHash(nameof(IsOff));
    }
    public static class InputData
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
    }
}
