using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public ScriptableData data;
    public Camera cam2;
    float cameraWidth, cameraHeight;

    const float width = 430f, height = 688f;

    // Use this for initialization
    void Awake()
    {
        switch (data.whichMode)
        {
            case 0:
                Camera.main.aspect = 10f / 16f;
                cam2.gameObject.SetActive(false);
                cameraWidth = width;
                cameraHeight = height;
                Screen.SetResolution((int)cameraWidth, (int)cameraHeight, false);
                Camera.main.rect = new Rect(0, 0, 1, 1);
                break;
            case 1:
                Camera.main.aspect = 10f / 16f;
                cam2.aspect = 10f / 16f;
                cam2.gameObject.SetActive(true);
                cameraWidth = 2 * width;
                cameraHeight = height;
                Screen.SetResolution((int)cameraWidth, (int)cameraHeight, false);
                Camera.main.rect = new Rect(0, 0, .5f, 1);
                cam2.rect = new Rect(.5f, 0, .5f, 1);
                break;
            case 2:
                Camera.main.aspect = 10f / 16f;
                cam2.gameObject.SetActive(false);
                cameraWidth = width;
                cameraHeight = height;
                Screen.SetResolution((int)cameraWidth, (int)cameraHeight, false);
                Camera.main.rect = new Rect(0, 0, 1, 1);
                break;
            default:
                break;
        }
    }

}
