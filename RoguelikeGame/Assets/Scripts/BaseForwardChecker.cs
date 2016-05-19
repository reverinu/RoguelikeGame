using UnityEngine;
using System.Collections;

public class BaseForwardChecker : MonoBehaviour {

    public struct DIRECTION
    {
        public static readonly int UP = 1;
        public static readonly int DOWN = -1;
        public static readonly int LEFT = 1;
        public static readonly int RIGHT = -1;
    }

    public struct DIRECTIONCHECK
    {
        public static readonly int UP = 0;
        public static readonly int DOWN = 1;
        public static readonly int LEFT = 2;
        public static readonly int RIGHT = 3;
        public static readonly int UPLEFT = 4;
        public static readonly int UPRIGHT = 5;
        public static readonly int DOWNLEFT = 6;
        public static readonly int DOWNRIGHT = 7;
    }
    
}
