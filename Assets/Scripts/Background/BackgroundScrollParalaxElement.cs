using UnityEngine;

namespace Background {
    public class BackgroundScrollParalaxElement : MonoBehaviour
    {
        [SerializeField] private float parallaxSpeedX;
        [SerializeField] private float parallaxSpeedY;
        
        private float length;
        private float startPosX, startPosY;
        private float relativeDistY;


        private void Start()
        {
            startPosX = transform.position.x;
            startPosY = transform.position.y;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }


        public void PosParalaxUpdate(float _time, float _cameraPosY)
        {
            #region X movment
            
            transform.Translate(-_time * parallaxSpeedX, 0, 0);
     
            if (transform.position.x < startPosX - length)
            {
                PosRepositionElement();
            }
            
            #endregion


            #region Y movement

            relativeDistY = _cameraPosY * parallaxSpeedY;
            transform.position = new Vector3(transform.position.x, startPosY + relativeDistY, transform.position.z);
            
            #endregion
        }

        private void PosRepositionElement()
        {
            transform.position = new Vector3(startPosX + length, transform.position.y, transform.position.z);
        }
    }
}