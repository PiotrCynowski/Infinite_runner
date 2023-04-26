using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageXPosLoop : MonoBehaviour
{
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private float xPosBckgImg;

    private bool isScrolling = true;


    private void Start()
    {
        ScrollSwitch(true);
    }

    private void Update()
    {
        if (isScrolling)
        {
            backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(xPosBckgImg, 0) * Time.deltaTime, backgroundImage.uvRect.size);
        }
    }

    private void ScrollSwitch(bool _isScrolling)
    {
        isScrolling = _isScrolling;
    }
}
