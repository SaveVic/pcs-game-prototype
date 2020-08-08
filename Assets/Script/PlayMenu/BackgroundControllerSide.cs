using UnityEngine;

public class BackgroundControllerSide : MonoBehaviour
{
    public GameObject[] listBG, listBGfromMain;
    public Camera cam;

    int midUp, midDown;
    int countBG;
    Vector3 translateBG;

    public static BackgroundControllerSide instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        midUp = 1;
        midDown = 2;
        translateBG = new Vector3(0, 32, 0);
        countBG = listBG.Length;
        CheckIntersection();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.transform.position.y > listBG[midUp].transform.position.y + 4)
        {
            midUp = (countBG + midUp - 1) % countBG;
            listBG[(countBG + midUp - 1) % countBG].transform.position += translateBG;
            midDown = (countBG + midDown - 1) % countBG;
            CheckIntersection();
        }
        else if (cam.transform.position.y < listBG[midDown].transform.position.y - 4)
        {
            midDown = (midDown + 1) % countBG;
            listBG[(midDown + 1) % countBG].transform.position -= translateBG;
            midUp = (midUp + 1) % countBG;
            CheckIntersection();
        }
    }

    public void CheckIntersection()
    {
        bool flag;
        for(int i=0; i<listBG.Length; i++)
        {
            flag = false;
            for(int j=0; j<listBGfromMain.Length; j++)
            {
                if(listBG[i].transform.position.y == listBGfromMain[j].transform.position.y)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                listBG[i].SetActive(false);
            }
            else
            {
                listBG[i].SetActive(true);
            }
        }
    }
}
