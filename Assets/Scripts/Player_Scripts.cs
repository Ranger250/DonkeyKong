using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Scripts : MonoBehaviour
{
    public bool inLadder;
    private bool jumped;
    private Rigidbody2D rig;
    public float jumpVel;
    public float moveSpeed;
    public float climbSpeed;
    public GameObject cur_plat;
    public Collider2D player_collider;

    // Start is called before the first frame update
    void Start()
    {
        player_collider = gameObject.GetComponent<Collider2D>();
        inLadder = false;
        jumped = false;
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") == 1f)
        {
            if (!jumped)
            {
                jumped = true;
                rig.velocity = new Vector2(rig.velocity.x, jumpVel);
            }
        }

        if (inLadder && Input.GetAxis("Vertical") < 0)
        {
            if (cur_plat != null)
            {
                StartCoroutine(disableCollision());
            }
        }
    }

    private void FixedUpdate()
    {
        

        if (Input.GetAxis("Horizontal") > .1 || Input.GetAxis("Horizontal") < -.1)
        {
            rig.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rig.velocity.y);
        }

        if (inLadder && (Input.GetAxis("Vertical") != 0))
        {
            rig.velocity = new Vector2(rig.velocity.x, Input.GetAxis("Vertical") * climbSpeed  );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            cur_plat = collision.gameObject;
            if (jumped)
            {
                jumped = false;
            }
        }
        if (collision.tag == "Ladder")
        {
            inLadder = true;
            rig.gravityScale = 0;
        }

        /*if (collision.tag == "Barrel")
        {
            SceneManager.LoadScene(0);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Platform"))
        {
            if (jumped)
            {
                jumped = false;
                cur_plat = collision.gameObject;
            }
        }*/
        if (collision.gameObject.CompareTag("Barrel"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            rig.gravityScale = 1;
            inLadder = false;
        }
        if (collision.tag == "Platform")
        {
            cur_plat = null;
        }
    }

    private IEnumerator disableCollision()
    {
        Collider2D platCollider = cur_plat.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(player_collider, platCollider);
        yield return new WaitForSeconds(0.55f);
        Physics2D.IgnoreCollision(player_collider, platCollider, false);
    }
}
