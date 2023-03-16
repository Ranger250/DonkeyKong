using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controler : MonoBehaviour
{
    private float Barrel_timer;
    public float Barrel_delay;
    public GameObject Barrel_Prefab;


    // Start is called before the first frame update
    void Start()
    {
        Barrel_timer = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        Barrel_timer -= Time.deltaTime;
        if (Barrel_timer <= 0 )
        {
            Barrel_timer = Barrel_delay;
            GameObject barrel = Instantiate(Barrel_Prefab);
        }
    }
}
