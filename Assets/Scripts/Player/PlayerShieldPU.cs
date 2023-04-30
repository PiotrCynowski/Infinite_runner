using UnityEngine;

namespace Player.PU
{
    public class PlayerShieldPU : MonoBehaviour
    {
        private void Awake()
        {
            PlayerInteractionCollision.onShieldPU += ActivateShield;
        }


        private void ActivateShield(int _timeDuration)
        {

        }
    }
}
