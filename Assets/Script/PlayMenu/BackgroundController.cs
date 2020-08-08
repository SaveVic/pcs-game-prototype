using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public GameObject[] listBG;

    int midUp, midDown;
    int countBG;
    Camera cam;
    Vector3 translateBG;

    // Use this for initialization
    void Start()
    {
        midUp = 1;
        midDown = 2;
        cam = Camera.main;
        translateBG = new Vector3(0, 32, 0);
        countBG = listBG.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.transform.position.y > listBG[midUp].transform.position.y + 4)
        {
            midUp = (countBG + midUp - 1) % countBG;
            listBG[(countBG + midUp - 1) % countBG].transform.position += translateBG;
            midDown = (countBG + midDown - 1) % countBG;
            BackgroundControllerSide.instance.CheckIntersection();
        }
        else if (cam.transform.position.y < listBG[midDown].transform.position.y - 4)
        {
            midDown = (midDown + 1) % countBG;
            listBG[(midDown + 1) % countBG].transform.position -= translateBG;
            midUp = (midUp + 1) % countBG;
            BackgroundControllerSide.instance.CheckIntersection();

        }
    }
}
