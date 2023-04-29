using UnityEngine;

namespace SpawnedCollObjects
{
    public class SpawnedTypePowerups : SpawnedCollObj, IAmPowerup
    {
        [SerializeField] private typeOfPowerup powerupType;

        [SerializeField] private float movingSpeed = 5f;
        [SerializeField] private int powerDuration;

        int IAmPowerup.powerDuration => powerDuration;

        public typeOfPowerup PowerUpType()
        {
            return powerupType;
        }

        void Update()
        {
            transform.Translate(Vector3.left * movingSpeed * Time.deltaTime);
        }
    }
}


public enum typeOfPowerup { Shield, SpeedUp };