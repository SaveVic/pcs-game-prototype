using UnityEngine;

public class DestroyOnOutside : MonoBehaviour
{
    public GameObject destructPlayerParticle;
    public bool solidObject;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8 && !solidObject && !GameControl.instance.immortal1)
        {
            GameControl.instance.GetDamage(1);
            if (!GameControl.instance.IsLifeRemaining(1))
            {
                DestructedEffect(destructPlayerParticle, other.gameObject);
                if(GameControl.instance.data.whichMode != 1)
                {
                    GameControl.instance.ShowRestartTextSingle();
                }
                else
                {
                    if (GameControl.instance.isWait)
                    {
                        GameControl.instance.ShowRestartTextMulti();
                    }
                    else
                    {
                        GameControl.instance.Wait(1);
                    }
                }
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.layer == 12 && !solidObject && !GameControl.instance.immortal2)
        {
            GameControl.instance.GetDamage(2);
            if (!GameControl.instance.IsLifeRemaining(2))
            {
                DestructedEffect(destructPlayerParticle, other.gameObject);
                if (GameControl.instance.data.whichMode != 1)
                {
                    GameControl.instance.ShowRestartTextSingle();
                }
                else
                {
                    if (GameControl.instance.isWait)
                    {
                        GameControl.instance.ShowRestartTextMulti();
                    }
                    else
                    {
                        GameControl.instance.Wait(2);
                    }
                }
                Destroy(other.gameObject);
            }
        }
    }

    void Update()
    {
        if (!GameControl.instance.isPause)
        {
            if (transform.position.y > ObstacleGenerator.viewObstacleAbove
            || transform.position.y < ObstacleGenerator.viewObstacleBelow)
            {
                Destroy(gameObject);
            }
        }
    }

    private void DestructedEffect(GameObject particle, GameObject obj)
    {
        Instantiate(particle, obj.transform.position, Quaternion.identity);
    }
}
