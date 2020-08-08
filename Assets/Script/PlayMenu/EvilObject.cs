    using UnityEngine;

public class EvilObject : MonoBehaviour
{
    public GameObject bullet;
    public float timeToSpawn;
    float timeNow;

    // Use this for initialization
    void Start()
    {
        timeNow = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeNow += Time.deltaTime;
        if (timeNow >= timeToSpawn && GameControl.instance.isAlive1 && GameControl.instance.isAlive2)
        {
            timeNow = 0f;
            Vector2 playerToEnemy = GetClosestPlayer();
            Vector2 dir = new Vector2(playerToEnemy.y, -playerToEnemy.x);
            float degree = GetDegreeRelative(dir);
            Vector3 directionBullet = new Vector3(0, 0, degree);
            Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(directionBullet));
        }
    }

    Vector3 GetClosestPlayer()
    {
        if (GameControl.instance.data.whichMode != 1)
        {
            if (PlayerController.instance != null)
                return (transform.position - PlayerController.instance.transform.position);
            else
                return new Vector3(0, -1, 0);
        }
        else
        {
            Vector3 tmp = (PlayerControllerSide.instance != null) ? PlayerControllerSide.instance.transform.position : new Vector3(0, -1, 0); ;
            if (PlayerController.instance != null)
            {
                float dist1 = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
                float dist2 = Vector3.Distance(transform.position, tmp);
                Vector3 vec = (dist1 < dist2) ? transform.position - PlayerController.instance.transform.position : transform.position - tmp;
                return vec;
            }
            else
                return transform.position - tmp;
        }
    }

    float GetDegreeRelative(Vector2 direction)
    {
        if (direction.x >= 0 && direction.y >= 0)
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
        else if (direction.x >= 0 && direction.y < 0)
        {
            return Mathf.Atan(direction.y / direction.x) * 180 / Mathf.PI;
        }
        else
            return 0;
    }
}

//



                        //
