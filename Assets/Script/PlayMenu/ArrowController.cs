using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arrow1, arrow2;
    public static ArrowController instance;

    Vector2 dir1, dir2;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //dir1 = dir2 = new Vector2(0, 0);
    }

    void Update()
    {
        if(PlayerController.instance != null) arrow1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetDegreeRelative(dir1 - (Vector2)arrow1.transform.position)));
        if (PlayerControllerSide.instance != null) arrow2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetDegreeRelative(dir2 - (Vector2)arrow2.transform.position)));
    }

    public void SetTarget(int i, Vector3 vec)
    {
        if(i == 1)
        {
            dir1 = vec;
        }
        else
        {
            dir2 = vec;
        }
    }

    float GetDegreeRelative(Vector2 direction)
    {
        if (direction.x > 0 && direction.y >= 0)
        {
            return Mathf.Atan(direction.y / direction.x) * 180 / Mathf.PI;
        }
        else if (direction.x < 0 && direction.y >= 0)
        {
            return Mathf.Atan(direction.y / direction.x) * 180 / Mathf.PI + 180;
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            return Mathf.Atan(direction.y / direction.x) * 180 / Mathf.PI - 180;
        }
        else if (direction.x > 0 && direction.y < 0)
        {
            return Mathf.Atan(direction.y / direction.x) * 180 / Mathf.PI;
        }
        else
        {
            if(direction.y >= 0)
            {
                return 90;
            }
            else
            {
                return -90;
            }
        }
    }
}
