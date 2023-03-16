using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Script : MonoBehaviour
{
    public float startSpeed;
    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(startSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BarrelEnd")
        {
            Destroy(gameObject);
        }
    }
}
