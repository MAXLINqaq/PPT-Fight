using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAStatus : PlayerStatus
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
        if (coll.gameObject.layer == 11)
        {
            hitPoint++;
        }
        if (coll.gameObject.layer == 12)
        {
            hitPoint++;
        }
    }
}
