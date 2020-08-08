using UnityEngine;

public class PlayerControllerSide : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject particle;
    public Vector2 force;
    public float maxForceTime;
    public float horizontalSpeed;
    public Camera cam;

    bool getForceUp;
    float holdForceTime;
    Border border;
    Animator immortalAnim;

    KeyCode boost;
    string movementX;

    public static PlayerControllerSide instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        boost = KeyCode.P;
        movementX = "Horizontal Side";

        rb = GetComponent<Rigidbody2D>();
        getForceUp = false;
        holdForceTime = 0f;
        border = new Border(
            cam,
            cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x,
            cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane)).x,
            cam.ViewportToWorldPoint(new Vector3(1, .75f, cam.nearClipPlane)).y,
            cam.ViewportToWorldPoint(new Vector3(0, .25f, cam.nearClipPlane)).y,
            1 / 3f
            );
        immortalAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FreezeVelocityX();
        if (Input.GetKey(boost))
        {
            if (!getForceUp)
            {
                particle.SetActive(true);
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            if (holdForceTime < maxForceTime)
            {
                getForceUp = true;
                rb.AddForce(force);
                holdForceTime += Time.deltaTime;
            }
            else
            {
                particle.SetActive(false);
            }
        }
        if (Input.GetKeyUp(boost))
        {
            particle.SetActive(false);
            getForceUp = false;
            holdForceTime = 0f;
        }
        if (Input.GetButton(movementX))
        {
            float moveX = Input.GetAxis(movementX);
            Vector3 move = new Vector3(moveX * Time.deltaTime * horizontalSpeed, 0, 0);
            move = transform.position + move;

            move.x = Mathf.Clamp(move.x, border.leftBorder + border.objectBorder, border.rightBorder - border.objectBorder);
            transform.position = move;
        }
        UpdateCamera();
    }

    void FreezeVelocityX()
    {
        if (rb.velocity.x != 0)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    void UpdateCamera()
    {
        float dif;
        if ((dif = transform.position.y + border.objectBorder - border.upperView) > 0)
        {
            cam.transform.position += new Vector3(0, dif, 0);
            border.UpdateBorder();
        }
        else if ((dif = transform.position.y - border.objectBorder - border.lowerView) < 0)
        {
            cam.transform.position += new Vector3(0, dif, 0);
            border.UpdateBorder();
        }
    }

    public void PlayAnimationImmortal()
    {
        immortalAnim.Play("Immortal");
    }
}
