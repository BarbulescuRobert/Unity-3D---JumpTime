using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    bool isGrounded;
    float Speed;
    float JumpSpeed;
    float SuperJumpSpeed;
    float SpringSpeed;
    int AirJumps = 1;
    int TotalJumps;
    Rigidbody2D rb;
    float maxHeight = -10f;

    public GameObject Cameraa;
    private float minX, maxX;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TotalJumps = AirJumps;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        TotalJumps = AirJumps;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Untagged")
        {
            if (collision.gameObject.tag == "Boost")
            {
                rb.AddForce(Vector2.up * SuperJumpSpeed);
                gameObject.tag = "Boosted";
            }
            else if (collision.gameObject.tag == "Spring")
            {
                FindObjectOfType<SpringScript>().StartSpringNow();
            }
            /*else if(collision.gameObject.tag == "Bird")
            {
                Debug.Log("Lovit");
                FindObjectOfType<UI_ManagerScript>().PlayerDied();
                this.gameObject.SetActive(false);
                GetComponent<WorldGenerator>().enabled = false;
            }*/
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && (isGrounded || TotalJumps > 0))
        {
            rb.AddForce(Vector2.up * JumpSpeed);
            TotalJumps--;
        }
        
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rb.transform.position += new Vector3(mousePosition.x, 0, 0) * Time.deltaTime * Speed;


        if (gameObject.tag == "Boosted")
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if (maxHeight < rb.transform.position.y)
                maxHeight = rb.transform.position.y;
            else if (maxHeight > rb.transform.position.y + 0.5f)
            {
                gameObject.tag = "Untagged";
                maxHeight = -10f;
            }
        }
        else
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }

        StayOnCamera();
    }

    private void StayOnCamera()
    {
        float camDistance = Vector3.Distance(transform.position, Cameraa.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;

        // Get current position
        Vector3 pos = transform.position;

        // Horizontal contraint
        if (pos.x < minX) pos.x = maxX;
        if (pos.x > maxX) pos.x = minX;

        // Update position
        transform.position = pos;
    }
    public void ModifySpeed(float val)
    {
        Speed = val;
    }

    public void ModifyJump(float val)
    {
        JumpSpeed = val;
    }
    public void ModifySuperJump(float val)
    {
        SuperJumpSpeed = val;
    }

    public void ModifySpringSpeed(float val)
    {
        SpringSpeed = val;
    }

    public void ModifyAirJumps(int val)
    {
        AirJumps = val;
    }

    public void BoostSpring()
    {
        rb.AddForce(Vector2.up * SpringSpeed);
        gameObject.tag = "Boosted";
    }

    public void SpringBought()
    {
        gameObject.transform.position = new Vector2(0.09f, 2.44f);
    }

    public void SpringNotBought()
    {
        gameObject.transform.position = new Vector2(0.09f, -3f);
    }
}
