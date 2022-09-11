using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBStatus : PlayerStatus
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = hitPoint.ToString();
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            hitPoint++;
        }
        if (coll.gameObject.layer == 9)
        {
            hitPoint++;
        }
    }

}
