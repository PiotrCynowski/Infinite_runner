using System;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionCollision : MonoBehaviour
    {
        public static event Action OnGameEnd;

        public delegate void OnPowerupShield(int _puDuration);
        public static event OnPowerupShield onShieldPU;

        public delegate void OnPowerupSpeedUp(int _puDuration);
        public static event OnPowerupSpeedUp onSpeedUpPU;


        private void OnTriggerEnter(Collider _other)
        {
            IAmPowerup powerup = _other.gameObject.GetComponent<IAmPowerup>();

            if (powerup != null)
            {
                UsePowerUp(powerup);
                powerup.PowerUpCollected();
                return;
            }

            OnGameEnd?.Invoke();
        }


        private void UsePowerUp(IAmPowerup _powerup)
        {
            switch (_powerup.PowerUpType())
            {
                case typeOfPowerup.Shield:
                    onShieldPU?.Invoke(_powerup.powerDuration);
                    break;
                case typeOfPowerup.SpeedUp:
                    onSpeedUpPU?.Invoke(_powerup.powerDuration);
                    break;
                default:
                    break;
            }
        }
    }
}


interface IAmPowerup
{
    int powerDuration { get; }
    typeOfPowerup PowerUpType();
    void PowerUpCollected();
}