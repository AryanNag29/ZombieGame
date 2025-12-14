using UnityEngine;

public class EnumStates : MonoBehaviour
{
    protected enum playerState
    {
     idle,
     moving,
     running
    }

    protected enum daynightState
    {
        day,
        night
    }
}
