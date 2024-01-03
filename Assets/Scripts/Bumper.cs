using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
    }
}
