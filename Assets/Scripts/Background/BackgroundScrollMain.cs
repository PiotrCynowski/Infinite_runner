using UnityEngine;

namespace Background
{
    public class BackgroundScrollMain : MonoBehaviour
    {
        [SerializeField] private BackgroundScrollParalaxElement[] elements;
        public Transform positionYReference;
        private int elementsSize;
        private float time, camYPos;


        private void Start()
        {
            elementsSize = elements.Length;
        }

        private void Update()
        {
            time = Time.deltaTime;
            camYPos = positionYReference != null ? positionYReference.position.y : 0;

            for (int i = 0; i < elementsSize; i++)
            {
                elements[i].PosParalaxUpdate(time, camYPos);
            }
        }
    }
}
