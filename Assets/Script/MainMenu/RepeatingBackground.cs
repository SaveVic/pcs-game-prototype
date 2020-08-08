using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{

    [Tooltip("vertical size of the sprite in the world space. Attach box collider2D to get the exact size")]
    public float verticalSize;
    float lowerCameraView;

    private void Start()
    {
        lowerCameraView = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
    }

    private void Update()
    {
        if (transform.position.y < lowerCameraView - verticalSize / 2) //if sprite goes down below the viewport move the object up above the viewport
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        Vector2 groundOffSet = new Vector2(0, verticalSize * 4f);
        transform.position = (Vector2)transform.position + groundOffSet;
    }
}
