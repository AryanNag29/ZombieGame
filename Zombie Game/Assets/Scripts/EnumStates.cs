using UnityEngine;

namespace ZombieGame
{
    public class EnumStates : MonoBehaviour
    {
        protected enum playerState
        {
            idle,
            walking,
            running
        }

        protected enum daynightState
        {
            day,
            night
        }
    }
}