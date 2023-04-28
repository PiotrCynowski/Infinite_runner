using UnityEngine;

namespace Player
{
    public class PlayerMouseMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float playerPosScreenEdge = 15f;
        private Vector3 mousePos;
        private float newYPos;
        private float screenTop, screenBottom;

        private void Start()
        {
            screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height- playerPosScreenEdge, 0)).y;
            screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, playerPosScreenEdge, 0)).y;
        }

        void Update()
        {
            Vector3 currentPos = transform.position;
            newYPos = Mathf.MoveTowards(currentPos.y, mousePos.y, movementSpeed * Time.deltaTime);

            newYPos = Mathf.Clamp(newYPos, screenBottom, screenTop);
            transform.position = new Vector3(currentPos.x, newYPos, currentPos.z);
        }
     

        public void ReceiveInput(Vector2 _mousePos)
        {
            mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
        }
    }
}
