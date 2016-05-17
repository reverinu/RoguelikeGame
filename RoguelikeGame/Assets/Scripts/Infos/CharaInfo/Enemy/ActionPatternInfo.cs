using UnityEngine;
using System.Collections;

public class ActionPatternInfo : MonoBehaviour {

    [Header("気付く距離")]
    public int distanceNotice;
    [Header("行動頻度")]
    public int actionFrequency;
    public enum PERSONALITY
    {
        STRONG,
        WUSS,
        MYPACE
    }
    public PERSONALITY personality;
}
