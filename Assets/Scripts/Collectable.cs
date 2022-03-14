using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    //Logic
    protected bool collected;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            onCollect();
        }
    }

    protected virtual void onCollect()
    {
        collected = true;
    }
}
