using UnityEngine;

public class DirectMoving : MonoBehaviour
{
    public GameObject destructBulletParticle, destructPlayerParticle;
    public float speed;
    public float lifetime;
    Vector3 direction;

    private void Start()
    {
        direction = -transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && !GameControl.instance.immortal1)
        {
            GameControl.instance.GetDamage(1);
            if (!GameControl.instance.IsLifeRemaining(1))
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
                        GameControl.instance.Wait(1);
                    }
                }
                Destroy(other.gameObject);
            }
            Destructed();
        }
        else if (other.gameObject.layer == 12 && !GameControl.instance.immortal2)
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
            Destructed();
        }
        else if (other.gameObject.layer == 9)
        {
            Destructed();
        }
    }

    void Destructed()
    {
        DestructedEffect(destructBulletParticle, gameObject);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!GameControl.instance.isPause)
        {
            transform.position += direction;
            if (lifetime <= 0)
            {
                Destructed();
            }
            lifetime -= Time.deltaTime;
        }
    }

    private void DestructedEffect(GameObject particle, GameObject obj)
    {
        Instantiate(particle, obj.transform.position, Quaternion.identity);
    }
}