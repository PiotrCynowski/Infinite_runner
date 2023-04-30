using UnityEngine;

namespace SpawnedCollObjects
{
    public class SpawnedTypePowerups : SpawnedCollObj, IAmPowerup
    {
        [SerializeField] private TypeOfPowerup powerupType;

        [SerializeField] private float movingSpeed = 5f;
        [SerializeField] private int powerDuration;


        #region powerup interface
        int IAmPowerup.PowerDuration => powerDuration;
    
        public TypeOfPowerup PowerUpType()
        {
            return powerupType;
        }
      
        public void PowerUpCollected()
        {
            ReturnToPool();
        }
        #endregion


        void Update()
        {
            transform.Translate(movingSpeed * Time.deltaTime * Vector3.left);
        }
    }
}


public enum TypeOfPowerup { Shield, SpeedUp };